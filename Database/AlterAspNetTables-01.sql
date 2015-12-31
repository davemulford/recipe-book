CREATE TABLE "AspNetUsers" (
  "Id" character varying(128) NOT NULL,
  "UserName" character varying(256) NOT NULL,
  "NormalizedUserName" character varying(256),
  "PasswordHash" character varying(256),
  "SecurityStamp" character varying(256),
  "Email" character varying(256) DEFAULT NULL::character varying,
  "NormalizedEmail" character varying(256) DEFAULT NULL::character varying,
  "EmailConfirmed" boolean NOT NULL DEFAULT false,
  "ConcurrencyStamp" character varying(256),
  "PhoneNumber" character varying(256),
  "PhoneNumberConfirmed" boolean NOT NULL DEFAULT false,
  "TwoFactorEnabled" boolean NOT NULL DEFAULT false,
  "LockoutEnd" date DEFAULT NULL,
  "LockoutEnabled" boolean NOT NULL DEFAULT false,
  "AccessFailedCount" integer,
  PRIMARY KEY ("Id")
);

ALTER TABLE "AspNetUserClaims"
  ADD CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_User_Id" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id")
  ON DELETE CASCADE;

ALTER TABLE "AspNetUserLogins"
  ADD CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id")
  ON DELETE CASCADE;

ALTER TABLE "AspNetUserRoles"
  ADD CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id")
  ON DELETE CASCADE;