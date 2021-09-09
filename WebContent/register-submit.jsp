<%@ page import="com.dell.webapp.dao.AdminDAO" %>
<%@ page import="com.dell.webapp.dao.UserDAO" %>
<%@ page import="com.dell.webapp.bean.Admin" %>
<%@ page import="com.dell.webapp.bean.User" %>
<%@ page import="java.util.List" %>
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
<%
String type = request.getParameter("type");
String username = request.getParameter("username");
String password = request.getParameter("password");

List<User> listOfUser = UserDAO.listUser();
List<Admin> listOfAdmin = AdminDAO.listAdmin();
int userflag = 0;
int adminflag = 0;
if(listOfUser.isEmpty()){
	userflag = 1;
}
if(listOfAdmin.isEmpty()){
	adminflag = 1;
}
if(type.equals("admin")){
	for(Admin ad: listOfAdmin){
		if(ad.getName().equals(username)){
			out.println("<h3 style='color:red;'>Admin already exists. Please create new Admin</h3>");
			adminflag = 0;
			break;
		}
		else{
			adminflag = 1;
		}
	}
	for(User us: listOfUser){
		if(us.getName().equals(username)){
			out.println("<h3 style='color:red;'>Admin requested is already a User. Please create new Admin</h3>");
			adminflag = 0;
			break;
		}
		else{
			adminflag = 1;
		}
	}

	if(adminflag == 1){
		Admin ad = new Admin(username,password);
		AdminDAO.addAdmin(ad);
		out.println("<h3 style='color:green;'>Admin is created successfully</h3>");
	}
}
else{
	
	for(User us: listOfUser){
		if(us.getName().equals(username)){
			out.println("<h3 style='color:red;'>User already exists. Please create new User</h3>");
			userflag = 0;
			break;
		}
		else{
			userflag = 1;
		}
	}
	for(Admin ad: listOfAdmin){
		if(ad.getName().equals(username)){
			out.println("<h3 style='color:red;'>User requested is already an Admin. Please create new User</h3>");
			userflag = 0;
			break;
		}
		else{
			userflag = 1;
		}
	}

	if(userflag == 1){
		User us = new User(username,password);
		UserDAO.addUser(us);
		out.println("<h1 style='color:green;'>User is created successfully</h1>");
	}
	
}



%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>