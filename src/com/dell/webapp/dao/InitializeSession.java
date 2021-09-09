package com.dell.webapp.dao;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.hibernate.Session;
import org.hibernate.SessionFactory;

import com.dell.webapp.util.HibernateSessionUtil;


/**
 * Servlet implementation class InitializeSession
 */
@WebServlet("/init")
public class InitializeSession extends HttpServlet {
	private static final long serialVersionUID = 1L;
	
    public InitializeSession() { }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		response.setContentType("text/html");
		PrintWriter out = response.getWriter();
		
		// build hibernate session factory
		try {
			// 1. load session factory 
			SessionFactory factory = HibernateSessionUtil.buildSessionFactory();
			
			// 2. create a session
			Session session = factory.openSession();
			
			if(session != null) {
				out.print("<h3 style='color:green'> Hibernate session is created sucessfully ! </h3>");
			}
			// close session
			session.close();
			
			String query = "SELECT * FROM Admin as ad";
			List listOfAdmins = session.createQuery(query).list();
			System.out.println(listOfAdmins);
		} catch (Exception e) {
			out.print("<h3 style='color:red'> Hibernate session is failed ! </h3>" + e);
		}
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		doGet(request, response);
	}

}