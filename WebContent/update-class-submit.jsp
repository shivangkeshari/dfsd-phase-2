<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="com.dell.webapp.bean.Classes" %>
<%@ page import="com.dell.webapp.dao.ClassDAO" %>
<%@ page import="java.util.List" %>
<!DOCTYPE html>
<html>
<head>
<meta charset="ISO-8859-1">
<title>Learner's Academy</title>
<jsp:include page="links.jsp"></jsp:include>
</head>
<body>
<jsp:include page="nav.jsp"></jsp:include>
<%
int id = Integer.parseInt(request.getParameter("id"));
String name = request.getParameter("name");
Classes cl = new Classes(id,name);
try{
	ClassDAO.updateClasses(cl);
	out.println("<h3 style='color:green;'>Class is updated successfully</h3>");
	response.setHeader("Refresh","1;url=manage-classes.jsp");
}
catch(Exception e)
{
	out.println("<h3 style='color:red;'>Class is not updated</h3>");
}

%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>