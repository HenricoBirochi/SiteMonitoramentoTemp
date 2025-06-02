function aplicaFiltroConsultaAvancada() {
    var vValorMedido = document.getElementById('valorMedido').value;
    var vDataInicial = document.getElementById('dataInicial').value;
    var vDataFinal = document.getElementById('dataFinal').value;
    $.ajax({
        url: "/Medida/ObtemDadosConsultaAvancada",
        data: {
            valorMedido: vValorMedido,
            dataInicial: vDataInicial,
            dataFinal: vDataFinal
        },
        success: function (dados) {
            if (dados.erro != undefined) {
                alert(dados.msg);
            }
            else {
                document.getElementById('resultadoConsulta').innerHTML = dados;
            }
        },
    });

}
