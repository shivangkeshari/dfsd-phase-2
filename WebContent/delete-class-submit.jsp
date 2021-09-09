<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="com.dell.webapp.bean.Classes" %>
<%@ page import="com.dell.webapp.dao.ClassDAO" %>
<%@ page import="com.dell.webapp.bean.Students" %>
<%@ page import="com.dell.webapp.dao.StudentDAO" %>
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
Classes cl = new Classes(id);
try{
	/* int studentId = 0;
	List<Students> listStuds = StudentDAO.listStudents();
	int flag = 0;
	for(Students std : listStuds){
		if(std.getClasses().getId() == id){
			studentId = std.getId();
			flag = 1;
			break;
		}
	}
	if(flag == 1){
		StudentDAO.deleteStudents(new Students(studentId));
	} */
	ClassDAO.deleteClasses(cl);
	out.println("<h3 style='color:green;'>Class is deleted successfully</h3>");
	response.setHeader("Refresh","1;url=manage-classes.jsp");
}
catch(Exception e){
	out.println("<h3 style='color:red;'>Class is not deleted</h3>");
}
%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>