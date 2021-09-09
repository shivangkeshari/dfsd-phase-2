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
					    Subjects
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">View Subjects</h5>
					    <p class="card-text">Read subjects from the master list</p>
					    <a href="view-subjects.jsp" class="btn btn-primary">View</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Subjects
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Add Subjects</h5>
					    <p class="card-text">Create subjects in the master list</p>
					    <a href="add-subjects.jsp" class="btn btn-primary">Add</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Subjects
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Update Subjects</h5>
					    <p class="card-text">Update subjects in the master list</p>
					    <a href="update-subjects.jsp" class="btn btn-primary">Update</a>
					  </div>
				</div>
			</div>
			<div class="col-md-2">
				<div class="card">
					  <div class="card-header">
					    Subjects
					  </div>
					  <div class="card-body">
					    <h5 class="card-title">Delete Subjects</h5>
					    <p class="card-text">Delete subjects from the master list</p>
					    <a href="delete-subjects.jsp" class="btn btn-primary">Delete</a>
					  </div>
				</div>
			</div>
		</div>	
	</div>
	<jsp:include page="footer.jsp"></jsp:include>
</body>
</html>
