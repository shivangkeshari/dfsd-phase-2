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
						List<Classes> listOfClasses = ClassDAO.listClasses();
						for(Classes cl: listOfClasses){
							out.print("<tr>");
							out.print("<td>"+cl.getId()+"</td>");
							out.print("<td>"+cl.getName()+"</td>");
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