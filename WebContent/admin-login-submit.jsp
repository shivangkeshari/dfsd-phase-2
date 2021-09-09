<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="com.dell.webapp.dao.AdminDAO" %>
<%@ page import="com.dell.webapp.bean.Admin" %>
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

List<Admin> listOfAdmins = AdminDAO.listAdmin();
for(Admin ad : listOfAdmins){
	if(ad.getName().equals(username) && ad.getPassword().equals(password)){
		int HOUR = 60*60;
		session.setAttribute("username", username);
		session.setMaxInactiveInterval(HOUR);
		out.println("<h3 style='color:green'>Admin is successfully logged in.</h3>");
		response.setHeader("Refresh","1;url=index.jsp");
	}
	else{
		out.println("<h3 style='color:red'>Admin is not registered. Please register the admin.</h3>");
	}
}
%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>