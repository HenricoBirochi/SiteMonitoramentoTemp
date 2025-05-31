// Função principal para buscar dados
function buscaDadosApi() {
    const domain = 'ec2-44-201-144-89.compute-1.amazonaws.com';
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
            // Cria nova linha na tabela
            const novaLinha = `
                <form class="form-medicao">
                    <tr>
                        <input name="MedidaId" value="${medidaId}" hidden>
                        <td><input name="ValorMedido" value="${dados.value}" readonly> °C</td>
                        <td><input name="HorarioMedicao" value="${dados.metadata.TimeInstant.value}" readonly></td>
                        <input name="DispositivoId" value="${dispositivoId}" hidden>
                        <td><button type="submit" class="btn btn-primary">Salvar Medida</button></td>
                    </tr>
                </form>
            `;

            // Adiciona no início da tabela
            $('#tabela-dados').prepend(novaLinha);

            // Limita a 10 registros
            if ($('#tabela-dados tr').length > 10) {
                $('#tabela-dados tr:last').remove();
            }
        },
        error: function (xhr, status, error) {
            console.error('Erro na requisição:', status, error);
            $('#loading').html('<td colspan="2">Erro ao carregar dados. Tentando novamente...</td>');
        }
    });
}

// Inicialização quando o documento estiver pronto
jQuery(function ($) {
    // Executa imediatamente
    buscaDadosApi();

    // Configura o intervalo de atualização (5 segundos)
    setInterval(buscaDadosApi, 5000);
});