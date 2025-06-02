// Variáveis globais
let horarios = [];
let temperaturas = [];
let grafico;

// Função para buscar dados da API
function buscaDadosApi() {

    const domain = 'ec2-13-218-19-179.compute-1.amazonaws.com';
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
            // Adiciona novo dado aos arrays
            horarios.push(dados.metadata.TimeInstant.value);
            temperaturas.push(dados.value);
            

            // Mantém apenas os últimos N pontos (ex: últimos 20)
            const maxPontos = 20;
            if (horarios.length > maxPontos) {
                horarios.shift();
                temperaturas.shift();
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
                        ${dados.metadata.TimeInstant.value}
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
        grafico.update();
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