(function ($) {

    showModal = function (id) {
        'use strict';
        $('#' + id).modal('show');
    };

    closePopup = function (id) {
        'use strict';
        $('#' + id).modal('hide');
    };

    showTab = function (id) {
        'use strict';
        $('#' + id).tab('show');
    };

    checkAll = function () {
        $(".chk-input").prop('checked', true);
    };


    showSuccessToast = function (msg) {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: msg,
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#46c35f',
            position: 'bottom-right'
        })
    };
    showInfoToast = function (msg) {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Info',
            text: msg,
            showHideTransition: 'slide',
            icon: 'info',
            loaderBg: '#57c7d4',
            position: 'bottom-right',
        })
    };
    showWarningToast = function (msg) {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Warning',
            text: msg,
            showHideTransition: 'slide',
            icon: 'warning',
            loaderBg: '#f2a654',
            position: 'bottom-right'
        })
    };
    showDangerToast = function (msg) {
        'use strict';
        resetToastPosition();
        $.toast({
            // heading: 'Error',
            text: msg,
            showHideTransition: 'slide',
            icon: 'error',
            loaderBg: '#f96868',
            position: 'bottom-right',
            hideAfter: 10000
        })
    };
    showToastPosition = function (position) {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Positioning',
            text: 'Specify the custom position object or use one of the predefined ones',
            position: String(position),
            icon: 'info',
            stack: false,
            loaderBg: '#f96868'
        })
    }
    showToastInCustomPosition = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Custom positioning',
            text: 'Specify the custom position object or use one of the predefined ones',
            icon: 'info',
            position: {
                left: 120,
                top: 120
            },
            stack: false,
            loaderBg: '#f96868'
        })
    }
    resetToastPosition = function () {
        $('.jq-toast-wrap').removeClass('bottom-left bottom-right top-left top-right mid-center'); // to remove previous position class
        $(".jq-toast-wrap").css({
            "top": "",
            "left": "",
            "bottom": "",
            "right": ""
        }); //to remove previous position style
    }


    FileUploadInit = function () {


        // Dropzone.options.myAwesomeDropzone = {
        //     uploadMultiple: false,
        //     maxFiles: 1,
        //     acceptedFiles: ".jpeg,.jpg,.png,.gif,.mp4,.mov,.wmv,.avi,.mkv.",


        //     init: function () {
        //         this.on("addedfile", function (event) {
        //             while (this.files.length > this.options.maxFiles) {
        //                 this.removeFile(this.files[0]);
        //             }
        //         });
        //     }
        // }

        Dropzone.options.myAwesomeDropzone = { // The camelized version of the ID of the form element

            // The configuration we've talked about above
            autoProcessQueue: false,
            uploadMultiple: true,
            parallelUploads: 100,
            maxFiles: 100,

            // The setting up of the dropzone
            init: function () {
                var myDropzone = this;

                // First change the button to actually tell Dropzone to process the queue.
                this.element.querySelector("button[type=submit]").addEventListener("click", function (e) {
                    // Make sure that the form isn't actually being sent.
                    e.preventDefault();
                    e.stopPropagation();
                    myDropzone.processQueue();
                });

                // Listen to the sendingmultiple event. In this case, it's the sendingmultiple event instead
                // of the sending event because uploadMultiple is set to true.
                this.on("sendingmultiple", function () {
                    // Gets triggered when the form is actually being sent.
                    // Hide the success button or the complete form.
                });
                this.on("successmultiple", function (files, response) {
                    // Gets triggered when the files have successfully been sent.
                    // Redirect user or notify of success.
                });
                this.on("errormultiple", function (files, response) {
                    // Gets triggered when there was an error sending the files.
                    // Maybe show form again, and notify user of error
                });
            }

        }

        window.jQuery.FileUpload.init();

    }

})(jQuery);