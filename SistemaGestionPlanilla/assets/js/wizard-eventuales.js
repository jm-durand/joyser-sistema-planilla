//== Class definition
var WizardDemo = function () {
    //== Base elements
    var wizardEl = $('#m_wizard');
    var formEl = $('#m_form');
    var validator;
    var wizard;

    //== Private functions
    var initWizard = function () {
        //== Initialize form wizard
        wizard = new mWizard('m_wizard', {
            startStep: 1
        });

        //== Validation before going to next page
        wizard.on('beforeNext', function (wizardObj) {
            if (validator.form() !== true) {
                wizardObj.stop();  // don't go to the next step
            }
        });


        //== Change event
        wizard.on('change', function (wizard) {
            mUtil.scrollTop();
           
       
        });
    };

    var initValidation = function () {
        validator = formEl.validate({
            //== Validate only visible fields
            ignore: ":hidden",

            //== Validation rules
            rules: {
                //=== Client Information(step 1)
                //== Client details
                name: {
                    required: true
                },
                apaterno: {
                    required: true
                },
                amaterno: {
                    required: true
                },
                tipdoc: {
                    required: true
                },
                numdoc: {
                    required: true,
                    number: true
                },
                sexos: {
                    required: true
                },
                estadociv: {
                    required: true
                },
                fnacimiento: {
                    required: true
                },
                tipplanilla: {
                    required: true
                },
                tiptrabajo: {
                    required: true
                },
                  tipaport: {
                    required: true
                },
                banco: {
                    required: true
                },
                numcuenta: {
                    required: true
                },
                cuentacts: {
                    required: true
                }
            },

            //== Validation messages
            messages: {
                'account_communication[]': {
                    required: 'You must select at least one communication option'
                },
                accept: {
                    required: "You must accept the Terms and Conditions agreement!"
                }
            },

            //== Display error  
            invalidHandler: function (event, validator) {
                mUtil.scrollTop();

                swal({
                    "title": "",
                    "text": "Debe completar los campos marcados con rojo.",
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                });
            },

            //== Submit valid form
            submitHandler: function (form) {

            }
        });
    };

    var initSubmit = function () {
        var btn = formEl.find('[data-wizard-action="submit"]');

        btn.on('click', function (e) {
            e.preventDefault();

            if (validator.form()) {
                //== See: src\js\framework\base\app.js
                mApp.progress(btn);
                //mApp.block(formEl); 



                ////== See: http://malsup.com/jquery/form/#ajaxSubmit
                //formEl.ajaxSubmit({
                //    success: function() {
                //        mApp.unprogress(btn);
                //        //mApp.unblock(formEl);

                //        swal({
                //            "title": "", 
                //            "text": "The application has been successfully submitted!", 
                //            "type": "success",
                //            "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                //        });
                //    }
                //});
            }
        });
    };

    return {
        // public functions
        init: function () {
            wizardEl = $('#m_wizard');
            formEl = $('#m_form');

            initWizard();
            initValidation();
            initSubmit();
        }
    };
}();

jQuery(document).ready(function () {
    WizardDemo.init();
});