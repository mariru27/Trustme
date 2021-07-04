# Trustme app

<!-- PROJECT LOGO -->
<p align="center">
  <a href="https://github.com/mariru27/Trustme">
    <img src="Trustme/wwwroot/images/logo_navbar_transparent.png" alt="Logo" width="400" height="400">
  </a>

    
Generator of cryptographic certificates for signing documents. 
Here you can see a demo (in romanian) ---> https://drive.google.com/file/d/1krjzHhJHSYDJBrg1pDYDhvHwFY9H_oph/view?fbclid=IwAR3amCebgrxOqtJj8ovZ6zi3C-eqCoi-JNgZc2jTBwy79eFFHMlPlrtfRxs



## Setup
If you want to run this application on your computer you need to do some things first.


In file appsettings.json you need to complete some fields (they are noted with prefix complete).

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
  
  ## Technology
 
  I used back-end framework ASP.NET CORE 3.1 (MVC), for front-end I used some javascript (just for pop-up), sql server database 2016. I used bootsrap (is a CSS framework) and html.
  
  
  ## Application main actions
  
  ![image](https://user-images.githubusercontent.com/46792157/124161796-81151180-daa6-11eb-85d6-97cee4eb66cf.png)

  After you registered and confirmed your email adress, you can login and do the next actions
  
  * Generate certificate
  * Upload document for a user (or for you) to sign
  * Sign a document
  * Verify (optional)
  
  ### Generate certificate
  
  Tha main purpose of generate certificate page is to generate a certificate and a private key, you can choose key size, "certificate name" and write a description for it.
  
  ![image](https://user-images.githubusercontent.com/46792157/124163103-e9b0be00-daa7-11eb-816a-a7bd9f77fc82.png)

  I used bouncy castle library for generating keys and certificates, and also for signing documents and for verifying it.
  
### Upload document
  
  When a user upload a document for another user, this will be notified on email and web app.
  User can choose a specific key that document needed to be sign with.
  
  ![image](https://user-images.githubusercontent.com/46792157/124164475-5a0c0f00-daa9-11eb-8bd3-ebfcc43baca5.png)
  
### Unsigned documents

  If the user upload for the first time a document for another user, this will need to allow that user to upload documents for him, after this in unsigned document page he will see all documents sent by that user.
  
  ![image](https://user-images.githubusercontent.com/46792157/124164819-cf77df80-daa9-11eb-824b-b8f96d4b8421.png)

  
  In Unsigned documents page user will have all documents that he need to sign. You can difference old by news documents by "new" label and green border.
  
  ![image](https://user-images.githubusercontent.com/46792157/124164968-f7ffd980-daa9-11eb-8272-99087f6147bb.png)

  ### Sign document
  
  If you want to sign a document, just click on sign button and you will be redirected to sign page. In sign page you will have some details about, document (you also can download document if you want to see it), and you will see the name of the key that you need to use to sign document.
  
  ![image](https://user-images.githubusercontent.com/46792157/124170053-a0fd0300-daaf-11eb-94ec-641e3042a5ac.png)

  
  ### Signed documents
  
  After you signed documents you will be redirected to signed document page, also the user that you signed document for will be notified on email and application, and signed document will appear in his "signed documents" page too.
  You will see this feature when you will use my app. :)))
  Now, when you will press details button, you will be redirected to details about signed document, there you will see the most important information in details, in special signature.
  
  ![image](https://user-images.githubusercontent.com/46792157/124170751-79f30100-dab0-11eb-98c4-9e9c22f49842.png)

 ### Verify signature
  Like I sayd in previous, verify signature is a optional action.
  For this you need to go to verify page, complete first step, choose a user that signed somethig, next you need to upload document and paste signature in the correct field (the single one also :DD).
  
  ![image](https://user-images.githubusercontent.com/46792157/124171569-698f5600-dab1-11eb-9e82-8fac881f2b58.png)

  

 ## Roles
  
  I have three roles:
  
  * User free
  * User pro
  * Administrator
  
  The difference between user pro and free is that user free can generate just three certificates and user pro can generate unlimited number.
  
 ### Administrator
  
  This role is more different than others, because this can not be created from application, this need to be created from database. Administrator can edit user information and delete users.
  
  ![image](https://user-images.githubusercontent.com/46792157/124174066-909b5700-dab4-11eb-944c-c087a29ee71f.png)

  
 ### User profile
  
  Talking about entities, users have their profile, from there they can see info about their certificates, modify name (certificate) and description, or delete it.
  
  ![image](https://user-images.githubusercontent.com/46792157/124172463-8d9f6700-dab2-11eb-8e8c-49d61dd5ee72.png)

  
  


  

