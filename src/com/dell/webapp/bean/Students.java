package com.dell.webapp.bean;

import javax.persistence.CascadeType;
import javax.persistence.JoinColumns;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.JoinTable;
import javax.persistence.OneToOne;
import javax.persistence.Table;

import org.hibernate.annotations.Fetch;
import org.hibernate.annotations.FetchMode;

@Entity
@Table(name= "student")
public class Students {
	
	@Id
	@GeneratedValue(strategy= GenerationType.AUTO)
	@Column(name = "id")
	private int id;
	
	@Column(name = "name")
	private String name;
	
	@OneToOne(cascade = CascadeType.ALL)
	@JoinColumn(name="class_id")
	private Classes classes;
	
	public Students() {
		super();
	}
	
	public Students(String name) {
		super();
		this.name = name;
	}
	
	public Students(int id, String name) {
		super();
		this.id = id;
		this.name = name;
	}

	public Students(int id) {
		super();
		this.id = id;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
	public Classes getClasses() {
		return classes;
	}

	public void setClasses(Classes classes) {
		this.classes = classes;
	}
	
	@Override
	public String toString() {
		return "Students [id=" + id + ", name=" + name + "]";
	}
	
}
