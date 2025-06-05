function aplicaFiltroConsultaAvancada() {
    let vValorMedido = document.getElementById('valorMedido').value;
    let vDataInicial = document.getElementById('dataInicial').value;
    let vDataFinal = document.getElementById('dataFinal').value;
    let vEstado = document.getElementById('estado').value;

    buscaDadosParaLista(vValorMedido, vDataInicial, vDataFinal, vEstado);
    graficoHistorico(vValorMedido, vDataInicial, vDataFinal, vEstado);
}

function buscaDadosParaLista(vValorMedido, vDataInicial, vDataFinal, vEstado) {
    $.ajax({
        url: "/Medida/ObtemDadosConsultaAvancada",
        data: {
            valorMedido: vValorMedido,
            dataInicial: vDataInicial,
            dataFinal: vDataFinal,
            estado: vEstado
        },
        success: function (dados) {
            if (dados.erro != undefined) {
                alert(dados.msg);
            }
            else {
                document.getElementById('resultadoConsulta').innerHTML = dados;
            }
        },
        error: function (err) {
            console.error("Erro na requisição:", err);
        }
    });
}

// Variáveis globais
let horarios1 = [];
let temperaturas1 = [];
let grafico1;
function graficoHistorico(vValorMedido, vDataInicial, vDataFinal, vEstado) {
    // Função para buscar dados da API
    $.ajax({
        url: '/Medida/ObtemDadosConsultaAvancadaJson',
        data: {
            valorMedido: vValorMedido,
            dataInicial: vDataInicial,
            dataFinal: vDataFinal,
            estado: vEstado
        },
        success: function (dados) {
            let invertido = dados.reverse();

            console.log("Dados recebidos do JSON:", invertido);

            if (invertido.erro) {
                console.log(invertido.msg);
                return;
            }
            
            // Antes de popular os dados
            horarios1 = [];
            temperaturas1 = [];

            // AQUI você já tem a lista e pode montar o gráfico
            invertido.forEach(item => {
                horarios1.push(item.horarioMedicao);
                temperaturas1.push(item.valorMedido);
            });

            atualizaGraficoHistorico();
        },
        error: function (err) {
            console.error("Erro ao buscar dados:", err);
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    let ctx1 = document.getElementById('graficoHistoricoTemperatura').getContext('2d');
    ctx1.canvas.width = 200;  // Largura em pixels
    ctx1.canvas.height = 200; // Altura em pixels
    grafico1 = new Chart(ctx1, {
        type: 'line',
        data: {
            labels: horarios1,
            datasets: [{
                label: 'Temperatura (ºC)',
                data: temperaturas1,
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
});

function atualizaGraficoHistorico() {
    grafico1.data.labels = horarios1;
    grafico1.data.datasets[0].data = temperaturas1;

    grafico1.update({
        duration: 800,
        easing: 'easeOutQuad'
    });
}