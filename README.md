# Trustme app

<!-- PROJECT LOGO -->
<p align="center">
  <a href="https://github.com/mariru27/Trustme">
    <img src="Trustme/wwwroot/images/logo_navbar_transparent.png" alt="Logo" width="400" height="400">
  </a>

    
License name - Generator of cryptographic certificates for signing documents. 



## Setup
If you want to run this application on your computer you need to do some things first.


In file appsettings.json you need to complete some fields (with prefix complete).

```sh
"NotificationMetadata": {
    "Sender": "complete_email_address",  //will be used to send email confirmation
    "SmtpServer": "smtp.gmail.com",
    "Port": 465,
    "FromUsername": "Trustme",
    "Password": "complete_email_password"
  },
  "PrivateKey": "complete_private_key",  
  ```

