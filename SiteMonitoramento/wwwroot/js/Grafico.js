// Variáveis globais
let horarios = [];
let temperaturas = [];
let grafico;

// Função para buscar dados da API
function buscaDadosApi() {

    const domain = 'ec2-3-89-90-143.compute-1.amazonaws.com';
    const linkApi = `http://${domain}:1026/v2/entities/urn:ngsi-ld:Temp:${dispositivoId}/attrs/temperature`;

    $.ajax({
        url: linkApi,
        method: 'GET',
        timeout: 20000,
        headers: {
            'fiware-service': 'smart',
            'fiware-servicepath': '/'
        },
        success: function (dados) {

            // Formata a data para o padrão brasileiro
            let data = new Date(dados.metadata.TimeInstant.value);
            let dataFormatada = data.toLocaleString('pt-BR', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric',
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit'
            })

            // Adiciona novo dado aos arrays
            horarios.push(dataFormatada);
            temperaturas.push(dados.value);

            // Atualiza erro e setpoint se malha fechada estiver ativa
            if (document.getElementById('malhaFechada').checked && setpoint !== null) {
                let erro = Math.abs(setpoint - dados.value);
                erros.push(erro);
                setpoints.push(setpoint);
            } else {
                erros.push(null);
                setpoints.push(null);
            }

            // Mantém apenas os últimos N pontos (ex: últimos 20)
            const maxPontos = 20;
            if (horarios.length > maxPontos) {
                horarios.shift();
                temperaturas.shift();
                erros.shift();
                setpoints.shift();
            }

            // Atualiza o gráfico
            atualizaGrafico();

            // Atualiza termomêtro
            atualizarGauge(dados.value);

            const novaLinha = `
                <tr>
                    <td>
                        ${dados.value}
                    </td>
                    <td>
                        ${dataFormatada}
                    </td>
                </tr>
            `;

            // Adiciona no início da tabela
            $('#tabela-dados').prepend(novaLinha);

            // Limita a 10 registros
            if ($('#tabela-dados tr').length > 10) {
                $('#tabela-dados tr:last').remove();
            }
        },
        error: function (err) {
            console.log('Erro ao buscar dados:', err);
        }
    });
}

// Função para atualizar o gráfico
function atualizaGrafico() {
    if (grafico) {
        grafico.data.labels = horarios;
        grafico.data.datasets[0].data = temperaturas;

        if (grafico.data.datasets.length > 1) {
            grafico.data.datasets[1].data = setpoints;
            grafico.data.datasets[2].data = erros;
        }

        grafico.update({
            duration: 800,
            easing: 'easeOutQuad'
        });
    }
}

// Função para atualizar o termomêtro
function atualizarGauge(temperatura) {
    if (termometroGauge) {
        termometroGauge.refresh(temperatura);
    }
}

// Inicializa o gráfico quando a página carrega
document.addEventListener('DOMContentLoaded', function () {
    //Gráfico
    let ctx = document.getElementById('graficoTemperatura').getContext('2d');
    ctx.canvas.width = 100;  // Largura em pixels
    ctx.canvas.height = 100; // Altura em pixels
    grafico = new Chart(ctx, {
        type: 'line',
        data: {
            labels: horarios,
            datasets: [{
                label: 'Temperatura (ºC)',
                data: temperaturas,
                borderColor: 'rgba(75, 192, 192, 1)',
                fill: false,
                tension: 0.2,
                pointRadius: 4,
                pointBackgroundColor: 'rgba(75, 192, 192, 1)',
            }]
        },
        options: {
            responsive: true,
            scales: {
                x: {
                    title: { display: true, text: 'Horário' }
                },
                y: {
                    title: { display: true, text: 'Temperatura (ºC)' }
                }
            }
        }
    });

    //Termometro
    termometroGauge = new JustGage({
        id: "termometro-gauge",
        value: 0,
        min: 0,
        max: 100,
        title: "Temperatura",
        label: "°C",
        levelColors: ["#00d5ff", "#ffa200", "#ff0008"],
        width: 200,
        height: 200
    });

    // Busca dados imediatamente e depois a cada 5 segundos
    buscaDadosApi();
    setInterval(buscaDadosApi, 1000);
});



// Erro e SetPoint
let setpoint = null;
let setpointInput = null;
let erros = [];
let setpoints = [];

document.getElementById('malhaAberta').addEventListener('change', function () {
    if (this.checked) {
        // Remove o input de setpoint se existir
        if (setpointInput) {
            setpointInput.remove();
            setpointInput = null;
        }

        // Remove as linhas de setpoint e erro do gráfico
        if (grafico.data.datasets.length > 1) {
            grafico.data.datasets.splice(1, 2);
            grafico.update();
        }

        setpoint = null;
        erros = [];
        setpoints = [];
    }
});

document.getElementById('malhaFechada').addEventListener('change', function () {
    if (this.checked && !setpointInput) {
        // Cria o input para o setpoint
        setpointInput = document.createElement('input');
        setpointInput.type = 'number';
        setpointInput.placeholder = 'Defina o Setpoint (ºC)';
        setpointInput.className = 'form-control mt-2';
        setpointInput.style.width = '200px';
        this.parentElement.appendChild(setpointInput);

        setpointInput.addEventListener('input', function () {
            const val = parseFloat(this.value);
            if (!isNaN(val)) {
                setpoint = val;
                atualizaGrafico(); // Recalcula os erros quando o setpoint muda
            }
        });

        // Preenche setpoints e erros com base nos dados existentes
        setpoints = horarios.map(() => setpoint ?? 0);
        erros = temperaturas.map(temp => Math.abs(temp - (setpoint ?? 0)));

        // Adiciona datasets de setpoint e erro no gráfico
        grafico.data.datasets.push({
            label: 'Setpoint',
            data: setpoints,
            borderColor: 'orange',
            borderDash: [5, 5],
            fill: false,
        });

        grafico.data.datasets.push({
            label: 'Erro |Temperatura - Setpoint|',
            data: erros,
            borderColor: 'red',
            fill: false,
        });

        grafico.update();
    }
});