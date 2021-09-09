package com.dell.webapp.util;

import org.hibernate.SessionFactory;
import org.hibernate.cfg.Configuration;

import com.dell.webapp.bean.Admin;
import com.dell.webapp.bean.Classes;
import com.dell.webapp.bean.Students;
import com.dell.webapp.bean.Subjects;
import com.dell.webapp.bean.Teachers;
import com.dell.webapp.bean.User;


public class HibernateSessionUtil {
	
	private static SessionFactory factory;
	
	public static SessionFactory buildSessionFactory() {
		
		factory = new Configuration().configure("hibernate.cfg.xml")
				.addAnnotatedClass(Admin.class)
				.addAnnotatedClass(User.class)
				.addAnnotatedClass(Subjects.class)
				.addAnnotatedClass(Students.class)
				.addAnnotatedClass(Teachers.class)
				.addAnnotatedClass(Classes.class)
				.buildSessionFactory();
		return factory;
	}

}
