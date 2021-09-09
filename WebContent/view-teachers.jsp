<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="com.dell.webapp.bean.Teachers" %>
<%@ page import="com.dell.webapp.dao.TeacherDAO" %>
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
			<table class="table table-striped">
				<thead>
					<th>ID</th>
					<th>Name</th>
				</thead>
				<tbody>
					<%
						List<Teachers> listOfTeachers = TeacherDAO.listTeachers();
						for(Teachers tea: listOfTeachers){
							if(!(tea.getName().equals(""))){
								out.print("<tr>");
								out.print("<td>"+tea.getId()+"</td>");
								out.print("<td>"+tea.getName()+"</td>");
								out.print("</tr>");
							}
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