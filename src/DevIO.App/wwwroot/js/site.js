function AjaxModal() {

    $(document).ready(function () {
        $.ajaxSetup({ cache: false });

        $("a[data-modal]").on("click",
            function (e) {
                $("#myModalContent").load(this.href,
                    function () {
                        $('#myModal').modal({
                            keyboard: true
                        },
                            'show');
                        bindForm(this);
                    });
                return false;
            });

        function bindForm(dialog) {
            $('form', dialog).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#myModal').modal('hide');
                            $('#EnderecoTarget').load(result.url);
                        } else {
                            $('#myModalContent').html(result);
                            bindForm(dialog);
                        }
                    }
                });
                return false;
            });
        }
    });
}

function BuscaCep() {
    $(document).ready(function () {

        function limpa_formulario_cep() {
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
        }

        $("#Endereco_Cep").blur(function () {

            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se o campo cep possui valor informado
            if (cep !== "") {

                //Expressão regular para validar o cep.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP
                if (validacep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");

                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {

                            if (!("erro" in dados)) {
                                $("#Endereco_Logradouro").val(dados.logradouro);
                                $("#Endereco_Bairro").val(dados.bairro);
                                $("#Endereco_Cidade").val(dados.localidade);
                                $("#Endereco_Estado").val(dados.uf);
                            } else {
                                limpa_formulario_cep();
                                alert("CEP não encontrado.");
                            }
                        });

                } else {
                    //cep errado
                    limpa_formulario_cep();
                    alert("Formato de CEP inválido.");
                }
            } else {
                //cep sem valor
                limpa_formulario_cep();
            }
        });

    });
}