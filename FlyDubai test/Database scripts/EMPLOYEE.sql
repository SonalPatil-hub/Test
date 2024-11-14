USE [EmployeeManagement]
GO

/****** Object:  Table [dbo].[Employee]    Script Date: 12-11-2024 11:54:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
	[EmployeeId] [varchar](15) NOT NULL,
	[FirstName] [varchar](35) NOT NULL,
	[LastName] [varchar](35) NULL,
	[EmpDeptId] [int] NULL,
	[Doj] [datetime] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Address] [varchar](500) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[ConfirmationDate] [datetime] NULL,
	[CreatedById] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedById] [varchar](50) NULL,
	[ModifiedDate]  [datetime] NULL
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[Doj] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


