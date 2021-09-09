package com.dell.webapp.dao;

import java.util.List;

import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;

import com.dell.webapp.bean.Teachers;
import com.dell.webapp.util.HibernateSessionUtil;

public class TeacherDAO {
	public static List<Teachers> listTeachers(){
		SessionFactory factory = HibernateSessionUtil.buildSessionFactory();
		Session session = factory.openSession();
		List listOfTeachers = session.createQuery("From Teachers as tea").list();
		return listOfTeachers;
	}
	
	public static void addTeachers(Teachers tea) {
		try {
			SessionFactory factory = HibernateSessionUtil.buildSessionFactory();
			Session session = factory.openSession();
			Transaction tx = session.beginTransaction();
			
			session.saveOrUpdate(tea);
			tx.commit();
			session.close();
		}
		catch(Exception e) {
			System.out.println("Add Teacher failed"+ e.getMessage().toString());
		}
	}
	
	public static void updateTeachers(Teachers tea) {
		try {
			SessionFactory factory = HibernateSessionUtil.buildSessionFactory();
			Session session = factory.openSession();
			Transaction tx = session.beginTransaction();
			
			session.update(tea);
			tx.commit();
			session.close();
		}
		catch(Exception e) {
			System.out.println("Update Teacher failed"+ e.getMessage().toString());
		}
	}
	
	public static void deleteTeachers(Teachers tea) {
		try {
			SessionFactory factory = HibernateSessionUtil.buildSessionFactory();
			Session session = factory.openSession();
			Transaction tx = session.beginTransaction();
			
			session.delete(tea);
			tx.commit();
			session.close();
		}
		catch(Exception e) {
			System.out.println("Delete Teacher failed"+ e.getMessage().toString());
		}
	}
}
