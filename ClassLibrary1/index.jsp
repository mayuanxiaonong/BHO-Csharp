<!DOCTYPE html>
<html>
<head>
<script type="text/javascript" src="http://apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js"></script>
<script type="text/javascript" src="http://apps.bdimg.com/libs/bootstrap/3.3.4/js/bootstrap.min.js"></script>
<link type="text/css" href="http://apps.bdimg.com/libs/bootstrap/3.3.4/css/bootstrap.css">
</head>
<body>
<h2>Hello World!</h2>

<form action="/index.jsp" method="get">
	<input id="username" name="username" />
	<input id="password" name="password" type="password" />
	<input type="submit" value="GET"/>
</form>
<hr />

<form action="/index.jsp" method="post">
	<input name="username" />
	<input name="password" type="password" />
	<input type="submit" value="POST"/>
</form>
<hr />

<button id="ajaxget">Ajax GET</button>
<button id="ajaxpost">Ajax POST</button>
<button id="extfun">External Function</button><br />
手机号码：<input id="mobile" /><br />
姓名：<input id="name" alt="姓名" /><br />
<a href="http://localhost:8080/index.jsp" target="_blank">Link</a>
<div id="ajaxdiv">
	<textarea id="count" value="0" cols="2000" type="hidden"></textarea>
</div>
<script type="text/javascript">
	$(document).ready(function() {
		$("#ajaxget").click(function() {
			ajax('/get.jsp', 'get');
		});

		$('#ajaxpost').click(function() {
			ajax('/post.jsp', 'post');
		});

		var ajax = function(url, type) {
			$.ajax({
				url: url,
				type: type,
				success: function(data) {
					$("#ajaxdiv").html(data);
				},
				error: function(e) {
					alert(e);
				}
			});
		}
	});
</script>
</body>
</html>
