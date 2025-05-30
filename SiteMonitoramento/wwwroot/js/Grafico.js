// Variáveis globais
let horarios = [];
let temperaturas = [];
let grafico;

// Função para buscar dados da API
function buscaDadosApi() {
    const domain = 'ec2-54-173-141-140.compute-1.amazonaws.com';
    const linkApi = `http://${domain}:1026/v2/entities/urn:ngsi-ld:Temp:${dispositivoId}/attrs/temperature`;

    $.ajax({
        url: linkApi,
        method: 'GET',
        timeout: 20000,
        headers: {
            'fiware-service': 'smart',
            'fiware-servicepath': '/',
            'accept': 'application/json'
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
        },
        error: function (err) {
            console.error('Erro ao buscar dados:', err);
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

// Inicializa o gráfico quando a página carrega
document.addEventListener('DOMContentLoaded', function () {
    let ctx = document.getElementById('graficoTemperatura').getContext('2d');
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

    // Busca dados imediatamente e depois a cada 5 segundos
    buscaDadosApi();
    setInterval(buscaDadosApi, 5000);
});