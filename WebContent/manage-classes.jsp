<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<jsp:include page="links.jsp"></jsp:include>
<title>Learner's Academy</title>
</head>
<body>
	<jsp:include page="nav.jsp"></jsp:include>
	<div class="container-fluid pt-4">
		<div class="row">
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Classes
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">View Classes</h5>
					    <p class="card-text">Read classes from the master list</p>
					    <a href="view-classes.jsp" class="btn btn-primary">View</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Classes
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Add Classes</h5>
					    <p class="card-text">Create classes in the master list</p>
					    <a href="add-class.jsp" class="btn btn-primary">Add</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Classes
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Add Classes With Teachers,Subjects</h5>
					    <p class="card-text">Create classes and assign subjects and teachers from the master list</p>
					    <a href="add-classes.jsp" class="btn btn-primary">Add</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Classes
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Update Classes</h5>
					    <p class="card-text">Update classes in the master list</p>
					    <a href="update-classes.jsp" class="btn btn-primary">Update</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Classes
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Delete Classes</h5>
					    <p class="card-text">Delete classes from the master list</p>
					    <a href="delete-classes.jsp" class="btn btn-primary">Delete</a>
					  </div>
				</div>
			</div>
		</div>	
	</div>
	<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>
