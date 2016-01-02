ALTER TABLE "AspNetRoles"
    ADD COLUMN "NormalizedName" character varying(256),
    ADD COLUMN "ConcurrencyStamp" character varying(256);