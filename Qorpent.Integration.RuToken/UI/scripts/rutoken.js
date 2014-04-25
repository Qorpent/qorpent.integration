var commonName = "testUsr";
(function() {
	var that = this;
	this.error = function(message) {
		// that.msg(message);
		// throw new Error(message);
		$('<p/>').text(message).miamodal({
			title: "Во время работы произошла ошибка",
			id: "rutoken-error-modal",
			width: 600,
			onClose: function() {
				that.TokenIntegration.RuToken.Start();
			}
		})
	},

	this.msg = function(message, 
		callback, preValue) {
		if (callback == null)  {
			alert(message);
		} else {
			callback(prompt(message, preValue || null));
		}
	},

	this.handlers = {
		statusWaiting: "rutoken_status_waiting",
		statusDeviceSearch: "rutoken_status_devicesearch",
		statusNotVerified: "rutoken_status_notverified",
		statusVerified: "rutoken_status_verified"
	},

	this.config = {
		"AllowRegistrationMode" : true,
		"RedirectPage" : "qorpent.start.html",
		"WhoamiPage" : "_sys/whoami.json.qweb",
		"SaltPage" : "rutoken/salt.json.qweb",
		"AuthPage" : "rutoken/auth.json.qweb",
		"CertSignPage" : "rutoken/sign.json.qweb",
		"DefaultPin" : "12345678",
		"DefaultKeyLabel" : "Zeus",
		"Stubs" : {
			"RuToken" : {
				"Registation" : {
					"subject" : [
						{"rdn" : "countryName", "value" : "RU"},
						{"rdn" : "stateOrProvinceName", "value" : "moscow"},
						{"rdn" : "localityName", "value" : "locality"},
						{"rdn" : "streetAddress", "value":  "street"},
						{"rdn" : "organizationName", "value" : "Aktiv"},
						{"rdn" : "organizationalUnitName", "value":  "IT"},
						{"rdn" : "title", "value" :  "должность"},
						{"rdn" : "commonName", "value" : commonName},
						{"rdn" : "postalAddress", "value":  "postal address"},
						{"rdn" : "pseudonym", "value":  "pseudonymus"},
						{"rdn" : "surname", "value" : "surname"},
						{"rdn" : "givenName", "value" : "given name"},
						{"rdn" : "emailAddress", "value" : "example@example.com"}
					],

					"extensions" : {
						"keyUsage" : [
							"digitalSignature", "nonRepudiation", "keyEncipherment",
							"dataEncipherment", "keyAgreement", "keyCertSign",
							"cRLSign", "encipherOnly", "decipherOnly"
						],

						"extKeyUsage" : [
							"emailProtection", "clientAuth", "serverAuth",
							"codeSigning", "timeStamping", "msCodeInd",
							"msCodeCom", "msCTLSign", "1.3.6.1.5.5.7.3.9",
							"1.2.643.2.2.34.6"
						]
					}
				}
			}
		},

		"RuToken" : {
			"Utils" : {
				"CleanUpDefaultAnswer" : "No"
			}
		}
	},

	this.dict = {
		"RuToken" : {
			"Init" : {
				"LoadFail" : "Не удалось загрузить RuToken Плагин. Переустановите плагин и проверьте системные параметры",
				"NoTokens" : "В системе не обнаружено действующих токенов!",
				"TooManyTokens" : "Внимание, конфликт токенов! Отключите лишние токены.",
				"EnumError" : "Сбой при попытке перечисления токенов с кодом: ",
				"LoginError" : "Сбой при попытке авторизации на токене с кодом: ",
				"EnumCertsError" : "Сбой при попытке перечисления сертификатов ключа: ",
				"AuthError" : "Сбой при попытке аутентификации с кодом: ",
				"NoCerts" : "В токене отсутствует требуемый сертификат"
			},

			"Registation" : {
				"KPErr" : "Ошибка при попытке создания ключевой пары с кодом: ",
				"GenCRTErr" : "Сбой при попытке генерации запроса сертификата с кодом: ",
				"CRTImportErr" : "Сбой при попытке импорта полученного сертификата с кодом: ",
				"GetCRTErr" : "Ошибка соединения с сервером при попытке получения сертификата",
				"LoginInput" : "Введите логин"
			},

			"Auth" : {
				"InputPin" : "Введите PIN-код: ",
				"WrongPin" : "Неверный PIN-код",
				"CantGetSalt" : "Сбой при попытке получения шифрующей строки"
			},

			"Utils" : {
				"CleanUpCanceled" : "Сброс состояния токена был отменён",
				"CleanUpAttention" : "Внимание! Сброс состояния токена безвозвратно уничтожит все ключи и сертификаты! Продолжить? [yes/no]",
				"EnumKeysError" : "Сбой при попытке перечисления хранимых ключей с кодом ошибки: ",
				"DelKeyError" : "Сбой при попытке удаления ключа с кодом ошибки: ",
				"DelCertError" : "Сбой при попытке удаления сертификата с кодом ошибки: ",
				"CleanUpDone" : "Сброс токена завершён успешно."
			}
		},
		
		"Sys" : {
			"NotAvailable" : "Система недоступна."
		}
	},

	this.TokenIntegration = {
		RuToken: {
			CheckTokenExists : function() {
				that.TokenIntegration.RuToken.Utils.EnumDevices(function(tokens) {
					if (Object.keys(tokens).length != 1) {
						$(window).trigger(that.handlers.statusWaiting);
						$(".zeus-app").removeClass("activated");
						$(".zeus-app").addClass("deactivated");
						that.TokenIntegration.RuToken.TryToStart();
					}
				});
			},
			
			TryToStart: function() {
				window.setTimeout(that.TokenIntegration.RuToken.Start, 5000);
				$(window).trigger(that.handlers.statusWaiting);
			},

			Init : function() {
				window.rti = that;
				that.TokenIntegration.RuToken.plugin = document.getElementById("plugin");

				if (!that.TokenIntegration.RuToken.plugin.valid) {
					that.error(that.dict["RuToken"]["Init"]["LoadFail"]);
				}
			},

			Load : function(callback) {
				that.TokenIntegration.RuToken.Utils.EnumDevices(callback);
			},

			Auth : function(token, pin, callback) {
				var authInternal = this;

				this.getSalt = function(callback) {
					$.ajax({
						url : that.config["SaltPage"],
						dataType : "json",
						success : callback,
						timeout : 1500
					}).error(
						function() {
							that.error(that.dict["RuToken"]["Auth"]["CantGetSalt"]);
						}
					);
				},

				this.authenticate = function(token, certId, salt, callback) {
					this.authenticateSuccess = function(key) {
						callback(key);
					},

					this.authenticateFailure = function(errorCode) {
						that.error(that.dict["RuToken"]["Init"]["AuthError"] + errorCode);
					};

					that.TokenIntegration.RuToken.plugin.authenticate(
						token["deviceId"], certId, salt, this.authenticateSuccess, this.authenticateFailure
					);
				},

				this.loginError = function(errorCode) {
					$(window).trigger(that.handlers.statusNotVerified);
					that.error(that.dict["RuToken"]["Init"]["LoginError"] + errorCode);
				},

				this.sendCredentials = function(credentals, callback) {
					$.ajax({
						url : that.config["AuthPage"],
						dataType : "json",
						success : callback,
						type : "POST",
						data : credentals,
						success : function(data) {
							$(window).trigger(that.handlers.statusVerified);
							callback({statusCode : ((data["auth"]) ? (200) : (403))});
						}
					}).error(
						function() {
							callback({statusCode : 403});
						}
					);
				},

				this.beginAuth = function(token, certs, salt, callback) {
					authInternal.authenticate(token, certs["0"]["certId"], salt, function(cms) {
						authInternal.sendCredentials({
							"Salt" : salt,
							"CertId" : certs["0"]["certId"],
							"Cms" : cms
						}, callback);
					});
				}

				this.loginSuccess = function() {
					that.TokenIntegration.RuToken.Utils.EnumCerts(token, function(certs) {
						authInternal.getSalt(function(salt) {
							if (!("0" in certs)) {
								if (that.config["AllowRegistrationMode"]) {
										that.TokenIntegration.RuToken.Registation(token, function(a) {
											that.TokenIntegration.RuToken.Utils.EnumCerts(token, function(certList) {
												authInternal.beginAuth(token, certList, salt, callback);
											});
										});
								} else {
									that.error(that.dict["RuToken"]["Init"]["NoCerts"]);
								}
							}
							authInternal.beginAuth(token, certs, salt, callback);
						})
					});
				};
				
				that.TokenIntegration.RuToken.plugin.login(token["deviceId"], pin, this.loginSuccess, this.loginError);
			},

			Start : function() {
				$(window).trigger(that.handlers.statusDeviceSearch);		that.TokenIntegration.RuToken.Init();
				that.TokenIntegration.RuToken.Load(function (tokens) {
					this.authHandler = function(state) {
						$(window).trigger(that.handlers.statusNotVerified);
						if (state.statusCode == 200) {
							
						} else {
							window.location.reload();
						}
					};

					var tokenCount = Object.keys(tokens).length;

					/*if (tokenCount <= 0) {
						that.error(that.dict["RuToken"]["Init"]["NoTokens"]);
					} else if (tokenCount > 1) {
						that.msg(that.dict["RuToken"]["Init"]["TooManyTokens"]);
					} else {
						that.msg(that.dict["RuToken"]["Auth"]["InputPin"], function(pin) {
							that.TokenIntegration.RuToken.Auth(tokens[0], pin, this.authHandler);
						}, that.config["DefaultPin"]);
					}*/

					if (tokenCount == 1) {
						$(window).trigger(that.handlers.statusNotVerified);
						var pininput = $('<input type="password" class="input" id="RuTokenAuthInputPin">');
						var content = $('<div/>').append(pininput);
						var modal = content.miamodal({
							title: "Введите пин",
							width: 250,
							id: "RuTokenAuthInputPinModal",
							customButton: {
								click: function() {
									$('#RuTokenAuthInputPinModal').trigger('hidden');
								},
								class: "btn-warning",
								text: "ОК"
							},
							onClose: function() {
								$(window).trigger(that.handlers.statusDeviceSearch);
								if (!$('#RuTokenAuthInputPin').val() || $('#RuTokenAuthInputPin').val() == "") {
									that.TokenIntegration.RuToken.Start();
								} else {
									that.TokenIntegration.RuToken.Auth(tokens[0], $('#RuTokenAuthInputPin').val(), this.authHandler);
								}
							}
						});
						pininput.focus();
						pininput.keyup(function(e) {
							if (e.keyCode == 13) {
								$('#RuTokenAuthInputPinModal').trigger('hidden');
							}
						});
					}
					else if (tokenCount > 1) {
						that.msg(that.dict["RuToken"]["Init"]["TooManyTokens"]);
					} else {
						that.TokenIntegration.RuToken.TryToStart();
					}
				});
			},

			Registation : function(token, callback) {
				var registationInternal = this;

				this.generateKeyPair = function(token, callback) {
					that.TokenIntegration.RuToken.plugin.generateKeyPair(
						token["deviceId"],
						"A",
						config["DefaultKeyLabel"],
						false,
						function(keyHexId) {callback(keyHexId);},
						function(errorCode) {that.error(that.dict["RuToken"]["Registation"]["KPErr"] + errorCode);}
					);
				},

				this.generateCertificateRequest = function(token, keyPairId, callback) {
					var content = $('<div/>').append(
						$('<input type="text" class="input input-normal" id="RuTokenAuthInputLogin">')
					);
					var modal = content.miamodal({
						title: "Введите ПИН",
						width: 200,
						id: "RuTokenAuthInputLoginModal",
						closebutton: false,
						custombutton: {
							click: function() {
								that.TokenIntegration.RuToken.plugin.createPkcs10(
									token["deviceId"],
									keyPairId, [
										{ "rdn": "countryName", "value" : "RU" },
										{ "rdn": "stateOrProvinceName", "value" : "moscow" },
										{ "rdn": "localityName", "value" : "locality" },
										{ "rdn": "streetAddress", "value":  "street" },
										{ "rdn": "organizationName", "value" : "Aktiv" },
										{ "rdn": "organizationalUnitName", "value":  "IT" },
										{ "rdn": "title", "value" :  "должность" },
										{ "rdn": "commonName", "value" : $('#RuTokenAuthInputLogin').val() },
										{ "rdn": "postalAddress", "value":  "postal address" },
										{ "rdn": "pseudonym", "value":  "pseudonymus" },
										{ "rdn": "surname", "value" : "surname" },
										{ "rdn": "givenName", "value" : "given name" },
										{ "rdn": "emailAddress", "value" : "example@example.com" }
									],
									that.config["Stubs"]["RuToken"]["Registation"]["extensions"],
									true,
									function(certRequest) {callback(certRequest)},
									function(errorCode) {that.error(that.dict["RuToken"]["Registation"]["GenCRTErr"] + errorCode);}
								);
								$('#RuTokenAuthInputLoginModal').trigger('hidden');
							}
						}
					});


					/*that.msg("Введите логин:", function(login) {
						that.TokenIntegration.RuToken.plugin.createPkcs10(
							token["deviceId"],
							keyPairId,
							[
							{"rdn" : "countryName", "value" : "RU"},
							{"rdn" : "stateOrProvinceName", "value" : "moscow"},
							{"rdn" : "localityName", "value" : "locality"},
							{"rdn" : "streetAddress", "value":  "street"},
							{"rdn" : "organizationName", "value" : "Aktiv"},
							{"rdn" : "organizationalUnitName", "value":  "IT"},
							{"rdn" : "title", "value" :  "должность"},
							{"rdn" : "commonName", "value" : login},
							{"rdn" : "postalAddress", "value":  "postal address"},
							{"rdn" : "pseudonym", "value":  "pseudonymus"},
							{"rdn" : "surname", "value" : "surname"},
							{"rdn" : "givenName", "value" : "given name"},
							{"rdn" : "emailAddress", "value" : "example@example.com"}
						],
							that.config["Stubs"]["RuToken"]["Registation"]["extensions"],
							true,
							function(certRequest) {callback(certRequest)},
							function(errorCode) {that.error(that.dict["RuToken"]["Registation"]["GenCRTErr"] + errorCode);}
						);
					});*/
				},

				this.importCertificate = function(token, certificate, callback) {
					that.TokenIntegration.RuToken.plugin.importCertificate(
						token["deviceId"],
						certificate,
						that.TokenIntegration.RuToken.plugin.CERT_CATEGORY_USER,
						function(certHexId) {callback(certHexId);},
						function(errorCode) {that.error(that.dict["RuToken"]["Registation"]["CRTImportErr"] + errorCode);}
					);
				},

				this.requestCertificate = function(token, callback) {
					registationInternal.generateKeyPair(token, function(keyHexId) {
						registationInternal.generateCertificateRequest(token, keyHexId, function(crs) {
							$.ajax({
								url : that.config["CertSignPage"],
								dataType : "json",
								type : "POST",
								data : {
									"CRS" : crs
								},
								success : function(data) {
									if (!("0" in data)) {
										that.error(that.dict["RuToken"]["Registation"]["GetCRTErr"]);
									}

									registationInternal.importCertificate(token, data["0"], function(certHexId) {
										callback(certHexId);
									})
								},
								timeout : 1500
							}).error(
								function(data) {
									that.error(that.dict["RuToken"]["Registation"]["GetCRTErr"]);
								}
							);
						});
					});
				};

				this.requestCertificate(token, function(certHexId) {
					callback(certHexId);
				});
			},

			Utils : {
				EnumCerts : function(token, callback) {
					this.enumCerts = function(certList) {
						var certs = new Object();
						$.each(certList, function(k, v) {
							certs[k] = new Object();
							certs[k]["certId"] = v;
						});

						callback(certs);
					},

					this.enumError = function(errorCode) {
						that.error(that.dict["RuToken"]["Init"]["EnumCertsError"] + errorCode);
					};

					plugin.enumerateCertificates(
						token["deviceId"],
						that.TokenIntegration.RuToken.plugin.CERT_CATEGORY_USER,
						this.enumCerts,
						this.enumError
					);
				},

				EnumKeys : function(token, callback) {
					this.enumKeys = function(keyList) {
						var keys = new Object();
						$.each(keyList, function(k, v) {
							keys[k] = new Object();
							keys[k]["keyId"] = v;
						});

						callback(keys);
					},

					this.enumError = function(errorCode) {
						that.error(that.dict["RuToken"]["Utils"]["EnumKeysError"] + errorCode);
					};

					plugin.enumerateKeys(
						token["deviceId"],
						"", // enum all keys
						this.enumKeys,
						this.enumError
					);
				},

				EnumDevices : function(callback) {
					var devices = new Object();
					this.enumSuccess = function(deviceList) {
						$.each(deviceList, function(k, v) {
							devices[v] = new Object();
							devices[v]["deviceId"] = v;
						});

						callback(devices);
					},

					this.enumFailure = function(errorCode) {
						that.error(that.dict["RuToken"]["Init"]["EnumError"] + errorCode);
					};

					that.TokenIntegration.RuToken.plugin.enumerateDevices(this.enumSuccess, this.enumFailure);
				},

				CleanUpToken : function(token, callback, withAttention) {
					this.cleanUpProcedure = function(token, callback) {
						that.TokenIntegration.RuToken.Utils.EnumCerts(token, function(certList) {
							var internal = this;
							this.success = function(data) {},
							this.failure = function(errorCode) {that.msg(that.dict["RuToken"]["Utils"]["DelCertError"] + errorCode)};

							$.each(certList, function(k, v) {
								that.TokenIntegration.RuToken.plugin.deleteCertificate(
									token["deviceId"], v["certId"], internal.success, internal.failure
								);
							});

							that.TokenIntegration.RuToken.Utils.EnumKeys(token, function(keyList) {
								var internal = this;
								this.success = function(data) {},
								this.failure = function(errorCode) {that.msg(that.dict["RuToken"]["Utils"]["DelKeyError"] + errorCode)};

								$.each(keyList, function(z, b) {
									that.TokenIntegration.RuToken.plugin.deleteKeyPair(
										token["deviceId"], b["keyId"], internal.success, internal.failure
									);
									
								});

								that.msg(that.dict["RuToken"]["Utils"]["CleanUpDone"]);
							});
						});
					};

					var cleanUpTokenInternal = this;
					withAttention = withAttention || false;

					if (withAttention) {
						that.msg(that.dict["RuToken"]["Utils"]["CleanUpAttention"], function(answer) {
							if (answer.toUpperCase() == "YES") {
								cleanUpTokenInternal.cleanUpProcedure(token, callback);
							} else {
								that.msg(that.dict["RuToken"]["Utils"]["CleanUpCanceled"])
							}
						}, that.config["RuToken"]["Utils"]["CleanUpDefaultAnswer"])
					} else {
						cleanUpTokenInternal.cleanUpProcedure(token, callback);
					}
				}
			}
		}
	},

	this.bootstrap = function() {
		$(document).ready(function() {
			$.ajax({
				url : that.config["WhoamiPage"],
				dataType : "json",
				success : function(json) {
					that.TokenIntegration.RuToken.Start();
				},
				error: function() {
					that.error(that.dict["Sys"]["NotAvailable"]);
				}
			});

			var statusElement = $('#rutokenStatus');
			var status = "waiting";
			$(window).on(that.handlers.statusDeviceSearch, function() {
				status = "devicesearch";
				statusElement.get(0).className = "rutoken-status devicesearch";
				$(".zeus-app").addClass("deactivated");
			});
			$(window).on(that.handlers.statusWaiting, function() {
				status = "waiting";
				statusElement.get(0).className = "rutoken-status waiting";
			});
			$(window).on(that.handlers.statusNotVerified, function() {
				status = "notverified";
				statusElement.get(0).className = "rutoken-status notverified";
			});
			$(window).on(that.handlers.statusVerified, function() {
				status = "verified";
				statusElement.get(0).className = "rutoken-status verified";
				window.setInterval(that.TokenIntegration.RuToken.CheckTokenExists,5000);
				// пока делаю все приложения доступными
				$(".zeus-app").removeClass("deactivated");
				$.ajax({
					url : that.config["WhoamiPage"],
					dataType : "json",
					success : function(json) {
						$("#rutokenLogon").text(json.logonname);
					}
				});
			});
			$(".zeus-app").click(function(e) {
				var url = $(e.target).attr("url");
				if (status == "verified" && !!url) {
					window.open(url, '_blank');
				}
			});
		});
	};

	this.bootstrap();
})();