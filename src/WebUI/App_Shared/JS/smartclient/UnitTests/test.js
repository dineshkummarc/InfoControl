$(document).ready(function() {

    var sandbox = $("<div />").hide().appendTo("Body");

    module("BASIC");

    test("AttrUp: Deve retornar o attributo desejado na pr�pria tag ou ir buscando nas tag pai", function() {
        var html = "<div><p><a command=\"click\" href=\"index.html\"></a></P></div>";
        var comm = sandbox.html(html).find("A").attrUp("command");
        equal(comm, "click");
    });

    test("AttrUp: Deve retornar o attributo desejado na pr�pria tag ou ir buscando nas tag pai", function() {
        var html = "<div command=\"click\" ><p><a href=\"index.html\"></a></P></div>";
        var comm = sandbox.html(html).find("A").attrUp("command");
        equal(comm, "click");
    });


    test("getAddress: Deve retornar o endere�o do recurso a ser acessado. HREF ", function() {
        var html = "<a command=\"click\" href=\"index.html\"></a>";
        var outerHtml = sandbox.html(html).find("A").getAddress();
        equal(outerHtml, "index.html");
    });

    test("getAddress: Deve retornar o endere�o do recurso a ser acessado. SOURCE ", function() {
        var html = "<a command=\"click\"  source='/infocontrol/SearchService.svc' " +
                 "params='{companyId:1, itemId:null}'></a>";
        var outerHtml = sandbox.html(html).find("A").getAddress();
        equal(outerHtml, "/infocontrol/SearchService.svc");
    });

    test("getAddress: Deve retornar o endere�o do recurso a ser acessado. SOURCE ends with slash ", function() {
        var html = "<a command=\"click\"  source='/infocontrol/SearchService.svc/' " +
                 "params='{companyId:1, itemId:null}'></a>";
        var outerHtml = sandbox.html(html).find("A").getAddress();
        equal(outerHtml, "/infocontrol/SearchService.svc");
    });


    test("getAddress: Deve retornar o endere�o do recurso a ser acessado. SOURCE w/ACTION ends with slash", function() {
        var html = "<a command=\"click\"  source='/infocontrol/SearchService.svc' " +
                "action='GetSampleData' params='{companyId:1, itemId:null}'></a>";
        var outerHtml = sandbox.html(html).find("A").getAddress();
        equal(outerHtml, "/infocontrol/SearchService.svc/GetSampleData");
    });

    test("getAddress: Deve retornar o endere�o do recurso a ser acessado. SOURCE w/ACTION ends with slash", function() {
        var html = "<a command=\"click\"  source='/infocontrol/SearchService.svc/' " +
                "action='GetSampleData' ></a>";
        var outerHtml = sandbox.html(html).find("A").getAddress();
        equal(outerHtml, "/infocontrol/SearchService.svc/GetSampleData");
    });

    module("COMMAND");

    test("Um elemento com atributo COMMAND deve disparar o evento de acordo com conte�do do atributo", function() {
        window.t = 0;
        sandbox.html("<p><div command='click' onbinding='window.t=2' /></p>")
               .initializeControls()
               .find("div")
               .click();
        equals(t, 2);

    });

    test("Um elemento com atributo COMMAND='click' deve ao ser clicado disparar o DataBind, result 2", function() {
        window["t"] = 0;
        sandbox.html("<a command='click' onbinding='window.t=2' />").find("A").dataBind();
        equals(t, 2);
    });

    test("Um elemento com atributo COMMAND='click' deve ao ser clicado disparar o DataBind, result 1", function() {
        window["t"] = 0;
        sandbox.html("<a command='click' onbinding='window.t=1' />").find("A").dataBind();
        equals(t, 1);
    });

    module("DATABIND");

    asyncTest("chama arquivos assincronamente BLANK.HTM", function() {
        stop();
        sandbox.html("<p><a command='click' href='../blank.htm' /></p>")
                .initializeControls()
                .find("A")
                .dataBind({
                    onsucess: function(result, status, request) {
                        ok(result.replace(/( )+/g, "") == "", "OK Vazio!");
                        start();
                    }
                });
    });

    asyncTest("chama arquivos assincronamente GRIDSAMPLE.ASCX", function() {
        stop();
        sandbox.html("<p><a command='click' href='../gridsample.ascx' /></p>")
               .initializeControls()
               .find("A")
               .dataBind({
                   onsucess: function(result, status, request) {
                       ok(result != "", "OK, Pegou o conte�do: " + result);
                       start();
                   }
               });
    });

    asyncTest("chama recursos de dados em JSON assincronamente", function() {
        stop();
        sandbox.html("<div><span command='click' source='/infocontrol/infocontrol/SearchService.svc/' " +
                     "action='GetSampleData' options='{parameters:{companyId:1, itemId:null}, formData:{}}' /></div>")
                .initializeControls()
                .find("span")
                .dataBind({
                    onsucess: function(result, status, request) {
                        ok(result.Data, "OK, Veio JSON: " + $.toJSON(result.Data));
                        start();
                    }
                });

    });


    asyncTest("TRIGGER: Ao disparar o DataBind deve chamar o DataBind do elemento que est� do atributo trigger", function() {
        window["t"] = 0;
        stop();
        var c = sandbox
                .html("<p id='test'  onbinding='t=2; ok(true, \"dataBind do Test\"); start();' /><p><a command='click' trigger='#test' /></p>")
                .initializeControls()
                .find("A")
                .dataBind();
    });


    asyncTest("TRIGGER: Ao disparar a tag A deve passar o parametro options", function() {
        stop();
        var c = sandbox
                .html("<p id='test'  onbinding='ok(options.data.teste, \"dataBind do Test: \" + options.data.teste); start();' /><p>" +
                      "<a command='click' trigger='#test' options='{\"teste\":\"ok\"}' /></p>")
                .initializeControls()
                .find("A")
                .dataBind();
    });


    asyncTest("TRIGGER: Ao disparar a tag A deve chamar o DataBind do elemento que est� do atributo trigger", function() {
        stop();
        var c = sandbox
                .html("<span id='test' command='click' source='/infocontrol/infocontrol/SearchService.svc/' action='GetSampleData' " +
                      " onsucess='ok(true, request.responseText); start();' " + 
                      " options='{parameters:{companyId:1, itemId:null}, formData:{}}'  ></span>" +
                      "<a command='click' trigger='#test'  ></a>")
                .initializeControls()
                .find("A")
                .dataBind();
    });

    module("NOT MODIFIED")

    asyncTest("Ajax Iframe: Ajax deve ter a capacidade de buscar arquivos atraves de Iframe para casos 304, VAZIO", function() {
        stop();
        sandbox.html("<P />")
               .ajaxIframe('../blank.htm', sandbox, function(result, status, xhr) {
                   ok(result === "");
                   start();
               });
    });

    asyncTest("Ajax Iframe: Ajax deve ter a capacidade de buscar arquivos atraves de Iframe para casos 304, FORMSAMPLE", function() {
        stop();
        sandbox.html("<P />")
               .ajaxIframe('../FormSample.ascx', sandbox, function(result, status, xhr) {
                   ok(result != "" && result != null, "OK, Pegou o conteudo: " + result);
                   start();
               });
    });


    asyncTest("Ajax deve ter a capacidade de buscar arquivos atraves de Iframe para casos 304", function() {
        stop();
        var div = sandbox.html("<div><p command='click' method='get' href='../blank.htm' /></div>")
                         .hide()
                         .initializeControls();

        // Http 200
        div.find("p").dataBind({
            onsucess: function() {
                //Http 304
                div.find("p").dataBind({
                    onsucess: function(result, status, xhr) {
                        equals(status, "notmodified");
                        ok(result == "");
                        start();
                    }
                });
            }
        });

    });

    asyncTest("DATABIND: Dispara um DataBind numa p�gina que retorna status http 304 ", function() {

        stop();
        expect(3);

        var div = sandbox.html("<div><p command='click' method='get' href='../blank.htm' /></div>").initializeControls();
        // Http 200
        div.find("p").dataBind({
            onsucess: function() {
                //Http 304
                div.find("p").dataBind({
                    onsucess: function(result, status, xhr) {

                        equals(status, "notmodified");
                        ok(xhr == null);
                        ok(result === "");
                        start();
                    }
                });
            }
        });
    });


    asyncTest("TARGET: Ao disparar o DataBind o retorno da requisi��o deve ser enviada para o controle identificado no atributo TARGET", function() {
        stop();
        expect(1);

        if ($("#test").size() == 0)
            $(document.body).prepend("<p id='test' />");
        $("#test").hide();

        sandbox.html("<p><a command='click' href='../formsample.ascx' target='#test' /></p>")
               .initializeControls()
               .find("A")
               .dataBind({
                   onsucess: function(result, status, request) {
                       ok($("#test").html(), "OK, Pegou o conte�do: " + $("#test").html());
                       start();
                   }
               })
               .closest("#test").hide();
    });

    test("Outer HTML: Deve retornar o conte�do do html incluindo ele mesmo", function() {
        var html = "<p><a command=\"click\" href=\"index.html\"></a></p>";
        var outerHtml = $(html).outerHtml();
        equal(outerHtml, html);
    });




    module("SCENARIO");

    test("GRID", function() {
        ok(false);
    });

    test("GRID Edit�vel", function() {
        ok(false);
    });

    test("Filter", function() {
        ok(false);
    });

    test("Paging", function() {
        ok(false);
    });

    test("Alfabetical Paging", function() {
        ok(false);
    });

    test("Wizard", function() {
        ok(false);
    });

    test("Form", function() {
        ok(false);
    });

    test("Graphics", function() {
        ok(false);
    });

    test("Mouseover Stylesheet", function() {
        ok(false);
    });

    test("Alert On Delete", function() {
        ok(false);
    });



});

