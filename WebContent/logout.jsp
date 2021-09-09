<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="ISO-8859-1">
<jsp:include page="links.jsp"></jsp:include>

<title>Learner's Academy</title>
</head>
<body>
<jsp:include page="nav.jsp"></jsp:include>
<%
		session.invalidate();
		out.println("<h3 style='color:green;'>User is logged out successfully!!</h3>");
		
%>
<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>