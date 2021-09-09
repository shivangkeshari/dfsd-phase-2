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
					    Students
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">View Students</h5>
					    <p class="card-text">Read students from the master list</p>
					    <a href="view-students.jsp" class="btn btn-primary">View</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Students
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Add Students</h5>
					    <p class="card-text">Create students in the master list</p>
					    <a href="add-students.jsp" class="btn btn-primary">Add</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Students
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Update Students</h5>
					    <p class="card-text">Update students in the master list</p>
					    <a href="update-students.jsp" class="btn btn-primary">Update</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Students
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Delete Students</h5>
					    <p class="card-text">Delete students from the master list</p>
					    <a href="delete-students.jsp" class="btn btn-primary">Delete</a>
					  </div>
				</div>
			</div>
		</div>	
	</div>
	<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>
