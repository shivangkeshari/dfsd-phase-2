<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="com.dell.webapp.bean.Students" %>
<%@ page import="com.dell.webapp.bean.Teachers" %>
<%@ page import="com.dell.webapp.bean.Subjects" %>
<%@ page import="com.dell.webapp.dao.StudentDAO" %>
<%@ page import="com.dell.webapp.bean.Classes" %>
<%@ page import="com.dell.webapp.dao.ClassDAO" %>
<%@ page import="java.util.List" %>
<%@ page import="java.util.Set" %>
<%@ page import="java.util.HashSet" %>
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
String studentName = request.getParameter("name");
int classId = Integer.parseInt(request.getParameter("class-id"));

String className ="";
String subjectName = "";
String teacherName = "";
int teacherId = 0;
int subjectId = 0;
int flag = 1;
List<Classes> listClass = ClassDAO.listClasses();
for(Classes cl : listClass){
	if(cl.getId() == classId){
		className = cl.getName();
		if(cl.getSubject() != null && cl.getTeacher() != null){
			subjectName = cl.getSubject().getName();
			teacherName = cl.getTeacher().getName();
			subjectId = cl.getSubject().getId();
			teacherId = cl.getTeacher().getId();
			flag = 1;
		}
		break;
	}
}

try{
	Students std = new Students(studentName);
	Classes cl1 = new Classes();
	if(flag == 1){
		cl1.setId(classId);
		cl1.setName(className);
		cl1.setSubject(new Subjects(subjectId, subjectName));
		cl1.setTeacher(new Teachers(teacherId,teacherName));
	}
	else{
		cl1.setId(classId);
		cl1.setName(className);
	}
	std.setClasses(cl1);
	StudentDAO.addStudents(std);
	out.println("<h3 style='color:green;'>Student is added successfully</h3>");
	response.setHeader("Refresh","1;url=manage-students.jsp");
}
catch(Exception e){
	System.out.println(e.getMessage().toString());
	System.out.println(e.getClass().toString());
	out.println("<h3 style='color:red;'>Student is not added</h3>");
}
%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>