<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="com.dell.webapp.bean.Subjects" %>
<%@ page import="com.dell.webapp.dao.SubjectDAO" %>
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
Subjects sub = new Subjects(id);
try{
	SubjectDAO.deleteSubjects(sub);
	out.println("<h3 style='color:green;'>Subject is deleted successfully</h3>");
	response.setHeader("Refresh","1;url=manage-subjects.jsp");
}
catch(Exception e){
	out.println("<h3 style='color:red;'>Subject is not deleted</h3>");
}
%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>