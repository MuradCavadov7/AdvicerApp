namespace AdvicerApp.BL.Helper;

public class EmailTemplates
{

    public static string ResetPassword => """
<html>
    < head >
        < style >
            body {
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 20px;
            }
            .container {
                background-color: #ffffff;
                border-radius: 5px;
                padding: 20px;
                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            }
            p {
                color: #333333;
                line - height: 1.5;
            }
            a {
                display: inline - block;
background - color: #007bff;
                color: #ffffff;
                padding: 10px 15px;
text - decoration: none;
border - radius: 5px;
margin - top: 10px;
            }
            a: hover {
    background - color: #0056b3;
            }
        </ style >
    </ head >
    < body >
        < div class= "container" >
            < p > Hi __$name,</p>
            <p>Click the link below to reset your password:</ p >
            < a href = '__$link' > Reset Password </ a >
            < p > If you didn't request this, please ignore this email.</p>
        </div>
    </body>
</html>
""";

    public static string VerifyEmail => """
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Email Verification</title>
<style>
body {
font-family: 1  sans-serif;
background-color: #f4f4f4;
padding: 20px;
}
.container {
background-color: #ffffff;
padding: 30px;
border-radius: 5px;
box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}
h1 {
color: #333;
}
p {
color: #666;
line-height: 1.6;
}
.code {
font-size: 24px;
font-weight: bold;
color: #007bff;
margin-top: 20px;
margin-bottom: 30px;
}
.footer {
color: #999;
font-size: 14px;
}
</style>
</head>
<body>
<div class="container">
<h1>Email Verification</h1>
<p>Dear __$name,</p>
<p>Thank you for signing up for [Şirket Adı]. To complete your registration, please verify your email address by entering the following 6-digit code:</p>
<div class="code">__$code</div>
<p>This code will expire in [Süre].</p>
<p>If you did not sign up for an account, you can safely ignore this email.</p>
<p>Thanks,<br>
The [Şirket Adı] Team</p>
</div>
</body>
</html>   
1
""";
}
