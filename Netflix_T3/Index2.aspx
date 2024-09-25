<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index2.aspx.cs" Inherits="Netflix_T3.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Home</title>
    <link rel="stylesheet" href="/css/style.css" />
    <link rel="stylesheet" href="css/style-try1.css" />
</head>
<body>
    <form id="form1">
    <header>
                <img class="logo" src="img/CoolCat_Greg.png">
                <nav>
                    <ul class="header_nav">
                        <li><a href="#"> Coffee</a></li>
                        <li> <a href="#">Coffee2</a></li>
                        <li> <a href="#">Coffee3</a></li>
                    </ul>
                </nav>
                <nav>
                    <label for="lenguage">Lenguage</label>
                    <select name="lenguage" id="lenguage">
                        <option value="english">English</option>
                        <option value="spanish">Español</option>
                    </select>
                    <a href=""> Contact</a>
                </nav>
    </header>
    <section>
        <p>Hello World</p>
        <a href="html/contact.html"><p>Hello Sr. Click here for another page</p></a>

        <h1>
            Intento con h1
        </h1>

        <div class="outrer">
            <div class="box">La caja inferior es del 90% - 30px</div>
        </div>
    </section>

    <footer class="footer">

    </footer>
        </form>
</body>
</html>
