package com.dell.webapp.dao;

import java.util.List;

import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;

import com.dell.webapp.bean.Students;
import com.dell.webapp.util.HibernateSessionUtil;

public class StudentDAO {
	public static List<Students> listStudents(){
		SessionFactory factory = HibernateSessionUtil.buildSessionFactory();
		Session session = factory.openSession();
		List listOfStudents = session.createQuery("From Students as std").list();
		return listOfStudents;
	}
	
	public static void addStudents(Students std) {
		try {
			SessionFactory factory = HibernateSessionUtil.buildSessionFactory();
			Session session = factory.openSession();
			Transaction tx = session.beginTransaction();
			
			session.saveOrUpdate(std);
			tx.commit();
			session.close();
		}
		catch(Exception e) {
			System.out.println("Add Student failed"+ e.getMessage().toString());
		}
	}
	
	public static void updateStudents(Students std) {
		try {
			SessionFactory factory = HibernateSessionUtil.buildSessionFactory();
			Session session = factory.openSession();
			Transaction tx = session.beginTransaction();
			
			session.update(std);
			tx.commit();
			session.close();
		}
		catch(Exception e) {
			System.out.println("Update Student failed"+ e.getMessage().toString());
		}
	}
	
	public static void deleteStudents(Students std) {
		try {
			SessionFactory factory = HibernateSessionUtil.buildSessionFactory();
			Session session = factory.openSession();
			Transaction tx = session.beginTransaction();
			
			session.delete(std);
			tx.commit();
			session.close();
		}
		catch(Exception e) {
			System.out.println("Delete Student failed"+ e.getMessage().toString());
		}
	}
}
