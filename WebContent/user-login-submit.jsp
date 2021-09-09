<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="com.dell.webapp.dao.UserDAO" %>
<%@ page import="com.dell.webapp.bean.User" %>
<%@ page import="java.util.List" %>
<!DOCTYPE html>
<html>
<head>
<meta charset="ISO-8859-1">
<jsp:include page="links.jsp"></jsp:include>

<title>Learner's Academy</title>
</head>
<body>
<jsp:include page="nav.jsp"></jsp:include>
<%
String username = request.getParameter("username");
String password = request.getParameter("password");

List<User> listOfUsers = UserDAO.listUser();
for(User us : listOfUsers){
	if(us.getName().equals(username) && us.getPassword().equals(password)){
		int HOUR = 60*60;
		session.setAttribute("username", username);
		session.setMaxInactiveInterval(HOUR);
		out.println("<h3 style='color:green'>User is successfully logged in.</h3>");
		response.setHeader("Refresh","1;url=index.jsp");
	}
	else{
		out.println("<h3 style='color:red'>User is not registered. Please register the user.</h3>");
	}
}
%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>