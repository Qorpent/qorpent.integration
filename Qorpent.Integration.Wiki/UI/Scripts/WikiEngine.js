(function (config) {
    var that = this;

    this.error = function (message, withException) {
        withException = withException || true;
        that.msg(message);

        if (withException) {
            throw new Error(message);
        }
    },

    this.msg = function (message, callback, preValue) {
        if (callback == null) {
            alert(message);
        } else {
            callback(prompt(message, preValue || null));
        }
    },

    this._storage = {
        "Phrases" : {
            "Errors" : {
                "LoadConetnt" : "Cannot load content!",
                "SaveContent" : "Cannot save content!"
            }
        },

        "Ajax" : {
            "Timeout": 1500
        },

        "Path" : {
            "LoadContent" : "/wikigp/load.json.qweb",
            "SaveContent" : "/wikigp/save.json.qweb"
        },

        "MainPage" : "/"
    },

    this.engine = {
        content : {
            load : function (code, callback) {
                $.ajax({
                    url : that._storage["Path"]["LoadContent"],
                    data : { "code" : code },
                    dataType : "json",
                    timeout : that._storage["Ajax"]["Timeout"],
                    success : callback
                }).error(function () {
                    that.error(that._storage["Phrases"]["Errors"]["LoadContent"])
                });
            },

            save : function (item, callback) {
                $.ajax({
                    url: that._storage["Config"]["Path"]["SaveContent"],
                    data: item,
                    dataType: "json",
                    timeout: that._storage["Config"]["Ajax"]["Timeout"],
                    success: callback
                }).error(function () {
                    that.error(that._storage["Config"]["Phrases"]["Errors"]["SaveContent"])
                });
            }
        },

        view : {
            page : function (item) {
                return window.qwiki.create(item["text"]);
            },

            html : function (item) {
                var page = that.engine.view.page(item);
                if ("html" in page) {
                    return page["html"];
                }

                return null;
            }
        }
    },

    this.interface = {
        ui : {
            drawWikiPage : function(item, html) {
                $("#page").html(html);
            },

            drawSkeleton : function () {
                $("body").append(
                    $("<div class='app-layout-header'></div>").append(
                        $("<div class='navbar'></div>").append(
                            $("<div class='navbar-inner'></div>")
                        )
                    ),
                    $("<div class='app-layout-block' id='page'></div>")
                );
            }
        },

        loadWikiPage : function (code, callback) {
            callback = callback || function (item) { };

            that.engine.content.load(code, function (item) {
                var html = that.engine.view.html(item);
                that.interface.ui.drawWikiPage(item, html);
                callback(item);
            });
        }
    }

    this.bootstrap = function (config) {
        this.declareWiki = function () {
            window.Wiki = that;
        },

        this.prepareWorkEnv = function () {
            that.interface.ui.drawSkeleton();
        },

        this.mergeConfig = function (config) {
            $.extend(that._storage, config);
        };

        this.mergeConfig(config);
        this.prepareWorkEnv();
        this.declareWiki();
       
        that.interface.loadWikiPage(_storage["MainPage"]);
    };

    $(document).ready(function () {
        that.bootstrap(config);
    });
})({
    "MainPage" : "/",

    "Path" : {
        "LoadContent": "/wikigp/get.json.qweb",
        "SavePage": "/wikigp/save.json.qweb"
    },

    "Ajax" : {
        "Timeout" : 1500
    },

    "Phrases": {
        "Errors": {
            "LoadContent" : "Сбой при попытке загрузить Wiki страницу",
            "SaveContent" : "Сбой при попытке сохранить Wiki страницу"
        }
    }
});