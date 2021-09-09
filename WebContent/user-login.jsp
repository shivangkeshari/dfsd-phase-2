<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="ISO-8859-1">
<jsp:include page="links.jsp"></jsp:include>
<title>Learner's Academy</title>
</head>
<body>
<jsp:include page="nav.jsp"></jsp:include>
<div class="container-fluid" id="div-bg">
	<div id="login">
        <div class="container">
            <div id="login-row" class="row justify-content-center align-items-center">
                <div id="login-column" class="col-md-6">
                    <div id="login-box" class="col-md-12">
                        <form id="login-form" class="form" action="user-login-submit.jsp" method="post">
                            <h3 class="text-center text-primary">User Login</h3>
                            <div class="form-group">
                                <label for="username" class="text-primary">Username:</label><br>
                                <input required type="text" name="username" id="username" class="form-control">
                            </div>
                            <div class="form-group">
                                <label for="password" class="text-primary">Password:</label><br>
                                <input required type="password" name="password" id="password" class="form-control">
                            </div>
                            <div class="form-group">
                            	<input type="submit" class="btn btn-primary btn-md" value="Login">
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>