# Trustme
Generator of cryptographic certificates for signing documents. 
Is an application that I developed for my license.

## Setup
If you want to run this application on your computer you need to do some things first.


In file appsettings.json you need to complete missin

```sh
"NotificationMetadata": {
    "Sender": "",
    "SmtpServer": "smtp.gmail.com",
    "Port": 465,
    "FromUsername": "Trustme",
    "Password": ""
  },
  "PrivateKey": "",
  ```

