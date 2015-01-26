/*The MIT License (MIT)

Copyright (c) The Other Guys Ltd http://whytheotherguys.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

(function ($) {
    $(document).ready(function () {
        $("#TOGContactForm_Submit").click(function (e) {
            e.preventDefault();

            var TOGContactForm_Name = $("#TOGContactForm_Name");
            var TOGContactForm_Email = $("#TOGContactForm_Email");
            var TOGContactForm_Message = $("#TOGContactForm_Message");
            var TOGContactForm_Submit = $("#TOGContactForm_Submit");
            var TOGContactForm_Alert = $("#TOGContactForm_Alert");
            var TOGContactForm_Alert_Message = $("#TOGContactForm_Alert_Message");
            var TOGContactForm_Success = $("#TOGContactForm_Success");
            var TOGContactForm_Success_Message = $("#TOGContactForm_Success_Message");

            TOGContactForm_Alert.hide();
            TOGContactForm_Success.hide();

            if (TOGContactForm_Name.val() == "") {
                TOGContactForm_Alert_Message.text("Please enter your name.");
                TOGContactForm_Alert.show();
                return false;
            }

            if (TOGContactForm_Email.val() == "" || !/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(TOGContactForm_Email.val())) {
                TOGContactForm_Alert_Message.text("Please enter a valid email address.");
                TOGContactForm_Alert.show();
                return false;
            }

            if (TOGContactForm_Message.val() == "") {
                TOGContactForm_Alert_Message.text("Please enter a message.");
                TOGContactForm_Alert.show();
                return false;
            }

            TOGContactForm_Submit.attr("disabled", "disabled");
            TOGContactForm_Submit.text("Sending..");

            var postData = {
                "Name": TOGContactForm_Name.val(),
                "Email": TOGContactForm_Email.val(),
                "Message": TOGContactForm_Message.val()
            };

            $.ajax({
                url: "/togcontactform/togcontactformsubmit",
                type: "post",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(postData),
                success: function (response) {
                    if (response.type == 'success') {
                        TOGContactForm_Success_Message.text("Thank you " + TOGContactForm_Name.val() + ", your message has been submitted to us.");
                        TOGContactForm_Success.show();
                        TOGContactForm_Name.val("");
                        TOGContactForm_Email.val("");
                        TOGContactForm_Message.val("");
                    }
                    else {
                        console.log(response.error);
                        TOGContactForm_Alert_Message.text("There was an error submitting the form, please try again later.");
                        TOGContactForm_Alert.show();
                    }
                    TOGContactForm_Submit.removeAttr("disabled");
                    TOGContactForm_Submit.text("Send");
                },
                error: function (response) {
                    TOGContactForm_Alert_Message.text("There was an error submitting the form, please try again later.");
                    TOGContactForm_Alert.show();
                    TOGContactForm_Submit.removeAttr("disabled");
                    TOGContactForm_Submit.text("Send");
                }
            });
        });
    });
})(jQuery);