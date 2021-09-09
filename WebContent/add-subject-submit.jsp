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
String name = request.getParameter("name");
Subjects sub = new Subjects(name);
try{
	SubjectDAO.addSubjects(sub);
	out.println("<h3 style='color:green;'>Subject is added successfully</h3>");
	response.setHeader("Refresh","1;url=manage-subjects.jsp");
}
catch(Exception e){
	out.println("<h3 style='color:red;'>Subject is not added</h3>");
}

%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>