CREATE TABLE "Recipes" (
  "Id" SERIAL NOT NULL CONSTRAINT PK_Recipe PRIMARY KEY,
  "UserId" VARCHAR(128) NOT NULL,
  "Name" VARCHAR(50) NOT NULL,  
  "RecipeText" TEXT
);

CREATE INDEX "IX_Recipe_Id" ON "Recipes" ("Id");

ALTER TABLE "Recipes"
  ADD CONSTRAINT "FK_Recipe_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id")
  ON DELETE CASCADE;