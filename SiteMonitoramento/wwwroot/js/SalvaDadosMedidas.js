$('.form-medicao').on('submit', function (e) {
    e.preventDefault(); // impede envio padrão

    let formulario = $(this);

    $.ajax({
        url: '/Medida/Save',      // Rota para sua action
        method: 'POST',
        data: formulario.serialize(),   // Converte todos os campos do form para dados de POST
        success: function (resposta) {
            alert(resposta.mensagem);
        },
        error: function () {
            alert('Erro ao enviar medição.');
        }
    });
});