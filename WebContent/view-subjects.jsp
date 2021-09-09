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
						List<Subjects> listOfSubjects = SubjectDAO.listSubjects();
						for(Subjects sub: listOfSubjects){
							if(!(sub.getName().equals(""))){
								out.print("<tr>");
								out.print("<td>"+sub.getId()+"</td>");
								out.print("<td>"+sub.getName()+"</td>");
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