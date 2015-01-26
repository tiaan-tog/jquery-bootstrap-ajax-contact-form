# Contact Form Plugin

Add a reference to the script file in your master page
```
<script src="~/Scripts/TOGContactForm.js"></script>
```

Add the partial view where you want to display the contact form
```
<div class="container">
    @Html.Partial("TOGContactForm")
</div>
```

Add these mail settings keys to your appSettings section in your Web.config file and change the values
```
<add key="mailPort" value="587" />
<add key="mailHost" value="smtp.gmail.com" />
<add key="mailEnableSsl" value="true" />
<add key="mailTimeout" value="10000" />
<add key="mailUseDefaultCredentials" value="false" />
<add key="mailUserName" value="yourname@gmail.com" />
<add key="mailPassword" value="yourpassword" />
<add key="mailFromAddress" value="yourname@gmail.com" />
```