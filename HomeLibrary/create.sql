use Library;
go

create table Section 
(
	id int not null identity(1,1),
	name nvarchar(100) not null,

	constraint PK_Section_id primary key (id)
);

create table Estimate
(
	id int not null identity(1,1),
	origin nvarchar(100) not null,
	[availability] bit not null,
	worth nvarchar(100) not null,
	recommendation nvarchar(200) not null,

	constraint PK_Estimate_id primary key (id)
);

create table Book
(
	id int not null identity(1,1),
	author nvarchar(100) not null, 
	title nvarchar(100) not null,
	edition nvarchar(100) not null,
	[year] int not null,
	pages int not null,
	section_id int not null,
	estimate_id int not null,

	constraint PK_Book_id primary key (id),
	constraint FK_Book_SectionId_Section_Id FOREIGN KEY (section_id) REFERENCES Section (id),
	constraint FK_Book_EstimateId_Estimete_Id FOREIGN KEY (estimate_id) REFERENCES Estimate (id)
);



create table SpecialLiterature
(
	id int not null identity(1,1),
	field nvarchar(100) not null, -- галузь

	constraint PK_SpecialLiterature_id primary key (id),
	constraint FK_SpecialLiterature_Id_Book_Id FOREIGN KEY (id) REFERENCES Book (id)
);

create table Schoolbook
(
	id int not null identity(1,1),
	form int not null, -- клас (в школі)
	[subject] nvarchar(100),

	constraint PK_Schoolbook_id primary key (id),
	constraint FK_Schoolbook_Id_Book_Id FOREIGN KEY (id) REFERENCES Book (id)
);

create table Detective
(
	id int not null identity(1,1),
	heroes_number int not null,

	constraint PK_Detective_id primary key (id),
	constraint FK_Detective_Id_Book_Id FOREIGN KEY (id) REFERENCES Book (id)
);