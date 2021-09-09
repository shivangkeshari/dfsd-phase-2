package com.dell.webapp.bean;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "teacher")
public class Teachers {

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	@Column(name = "id")
	private int id;
	
	@Column(name = "name")
	private String name;
	
	public Teachers() {
		super();
	}
	
	public Teachers(String name) {
		super();
		this.name = name;
	}
	
	public Teachers(int id, String name) {
		super();
		this.id = id;
		this.name = name;
	}
	
	public Teachers(int id) {
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
	
	@Override
	public String toString() {
		return "Teachers [id=" + id + ", name=" + name + "]";
	}
}