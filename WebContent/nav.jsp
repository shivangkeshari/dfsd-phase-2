<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="core" %>
<!DOCTYPE html>
<html>
<nav class="navbar navbar-expand-sm bg navbar-dark">
  <ul class="navbar-nav">
    <li class="nav-item"><a href="index.jsp" class="navbar-brand" style="color:white;">Learner's Academy</a></li>
    <li class="nav-item">
      <a class="nav-link" href="index.jsp">Home</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="register.jsp">Register</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="admin-login.jsp">Admin Login</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="user-login.jsp">User Login</a>
    </li>
    
    <li class="nav-item">
      <a class="nav-link" href="init">Initialize</a>
    </li>
    
    <%
	    String user = (String)session.getAttribute("username");
	    if(user != null) {
	    	out.println("<li class='nav-item'><a class='nav-link' href='logout.jsp'>Logout</a></li>");
	    }
    %>
  </ul>
</nav>
</html>
