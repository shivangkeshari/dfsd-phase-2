<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="com.dell.webapp.bean.Classes" %>
<%@ page import="com.dell.webapp.bean.Subjects" %>
<%@ page import="com.dell.webapp.bean.Teachers" %>
<%@ page import="com.dell.webapp.bean.Students" %>
<%@ page import="com.dell.webapp.dao.ClassDAO" %>
<%@ page import="com.dell.webapp.dao.SubjectDAO" %>
<%@ page import="com.dell.webapp.dao.TeacherDAO" %>
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
int classId= Integer.parseInt(request.getParameter("class-id"));
int subjectId =  Integer.parseInt(request.getParameter("subject-id"));
int teacherId = Integer.parseInt(request.getParameter("teacher-id"));

String className = "";
String subjectName = "";
String teacherName = "";

List<Classes> listClass= ClassDAO.listClasses();
for(Classes cl : listClass){
	if(cl.getId() == classId){
		className = cl.getName();
		break;
	}
}

List<Subjects> listSubs = SubjectDAO.listSubjects();
for(Subjects sub : listSubs){
	if(sub.getId() == subjectId){
		subjectName = sub.getName();
		break;
	}
}

List<Teachers> listTeacher = TeacherDAO.listTeachers();
for(Teachers tea : listTeacher){
	if(tea.getId() == teacherId){
		teacherName = tea.getName();
		break;
	}
}

if(className != null || !(className.equals(""))){
	Classes cl1 = new Classes(classId,className);
	cl1.setSubject(new Subjects(subjectId,subjectName));
	cl1.setTeacher(new Teachers(teacherId,teacherName));

	try{
		ClassDAO.addClasses(cl1);
		out.println("<h3 style='color:green;'>Class is added successfully</h3>");
		response.setHeader("Refresh","1;url=manage-classes.jsp");
	}
	catch(Exception e){
		out.println("<h3 style='color:red;'>Class is not added</h3>");
	}
}
%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>