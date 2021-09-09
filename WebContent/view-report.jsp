<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="com.dell.webapp.bean.Students" %>
<%@ page import="com.dell.webapp.dao.StudentDAO" %>
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

<div class="container-fluid div-bg pt-5" >
	<div class="row">
		<div class="col-md-3"></div>
		<div class="col-md-6">
			<h1>Report</h1>
			<table class="table table-striped">
				<thead>
					<th>CLass</th>
					<th>Subject</th>
					<th>Teacher</th>
					<th>Student</th>
				</thead>
				<tbody>
					<%
						
						int flag =0;
						List<Classes> listClass = ClassDAO.listClasses();

						List<Students> listOfStudents = StudentDAO.listStudents();
						
						for(Classes cl: listClass){
							String studentName = "";
							out.print("<tr>");
							out.print("<td>"+cl.getName()+"</td>");
							if(cl.getSubject() != null){
								out.print("<td>"+cl.getSubject().getName()+"</td>");
							}
							else{
								out.print("<td></td>");
							}
							if(cl.getTeacher() != null){
								out.print("<td>"+cl.getTeacher().getName()+"</td>");
							}
							else{
								out.print("<td></td>");
							}
							for(Students s : listOfStudents){
								if(s.getClasses().getId() == cl.getId()){
									studentName = studentName+"  "+s.getName();
								}	
							}
							out.print("<td>"+studentName+"</td>");
							out.print("</tr>");
						}
					%>
				</tbody>
			</table>
		</div>
		<div class="col-md-3"></div>
	</div>
</div>

<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>