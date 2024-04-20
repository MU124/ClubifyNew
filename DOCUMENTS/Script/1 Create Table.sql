CREATE TABLE Settings (
    club_name VARCHAR(50),
    logo NVARCHAR(500),
    email VARCHAR(100),
    number VARCHAR(50),
    about_us NVARCHAR(MAX)
);

CREATE TABLE Player (
    player_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(100),
    date_of_birth DATE,
    profile_pic NVARCHAR(500),
    role_id INT,
    password VARCHAR(50),
    is_active BIT,
    created_date DATETIME,
    created_by INT,
    modified_date DATETIME,
    modified_by INT
);

CREATE TABLE Teams (
    team_id INT PRIMARY KEY,
    team_name VARCHAR(50),
    captain_id INT,
    logo NVARCHAR(500),
    description VARCHAR(500),
    created_date DATETIME,
    created_by INT,
    modified_date DATETIME,
    modified_by INT
);

CREATE TABLE Team_Player (
    team_player_id INT PRIMARY KEY,
    team_id INT,
    player_id INT,
    created_date DATETIME,
    created_by INT,
    modified_date DATETIME,
    modified_by INT,
    FOREIGN KEY (team_id) REFERENCES Teams(team_id),
    FOREIGN KEY (player_id) REFERENCES Player(player_id)
);

CREATE TABLE Match (
    match_id INT PRIMARY KEY,
    match_date DATE,
    location VARCHAR(100),
    fees NUMERIC(18, 2),
    team1_id INT,
    team2_id INT,
    winner_team_id INT,
    created_date DATETIME,
    created_by INT,
    modified_date DATETIME,
    modified_by INT,
    FOREIGN KEY (team1_id) REFERENCES Teams(team_id),
    FOREIGN KEY (team2_id) REFERENCES Teams(team_id),
    FOREIGN KEY (winner_team_id) REFERENCES Teams(team_id)
);

CREATE TABLE Gender (
    gender_id INT PRIMARY KEY,
    gender_name VARCHAR(50)
);

CREATE TABLE Role (
    role_id INT PRIMARY KEY,
    role_name VARCHAR(50)
);
