CREATE TYPE task_state AS ENUM ('NOT_STARTED', 'STARTED', 'COMPLETED');
CREATE TYPE note_type AS ENUM ('QEUSTION', 'INFO', 'NEW_WORK', 'COMPLAINT');
CREATE TYPE permission_type AS ENUM ('ADMIN', 'USER');

-- Account Table
CREATE TABLE account (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name varchar NOT NULL,
    email varchar UNIQUE NOT NULL,
    password varchar NOT NULL,
    permission_type varchar NOT NULL,
    picture varchar,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Journal Table
CREATE TABLE journal (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    color varchar not null,
    account_id UUID NOT NULL,
    title varchar NOT NULL,
    description varchar,
    current_sprint varchar,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (account_id) REFERENCES account(id) ON DELETE CASCADE
);

-- Journal Entry Table
CREATE TABLE journal_entry (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    journal_id UUID NOT NULL,
    sprint varchar,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (journal_id) REFERENCES journal(id) ON DELETE CASCADE
);

-- Note Table
CREATE TABLE note (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    journal_entry_id UUID NOT NULL,
    content text NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (journal_entry_id) REFERENCES journal_entry(id) ON DELETE CASCADE
);

-- Task Table
CREATE TABLE task (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    journal_entry_id UUID NOT NULL,
    title varchar NOT NULL,
    description text,
    status varchar NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    finished_at TIMESTAMP.
    FOREIGN KEY (journal_entry_id) REFERENCES journal_entry(id) ON DELETE CASCADE
);

-- Review Table (One-to-One with Journal Entry)
CREATE TABLE review (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    journal_entry_id UUID UNIQUE NOT NULL,
    content text NOT NULL,
    rating integer,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (journal_entry_id) REFERENCES journal_entry(id) ON DELETE CASCADE
);

-- Scratch Pad Table (One-to-One with Task)
CREATE TABLE scratch_pad (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    task_id UUID UNIQUE NOT NULL,
    content text,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (task_id) REFERENCES task(id) ON DELETE CASCADE
);

-- Queries to fetch required data:
-- 1. Get all tasks, notes, scratch pads, and journal entries belonging to a journal
SELECT je.*, t.*, n.*, sp.*
FROM journal_entry je
LEFT JOIN task t ON t.journal_entry_id = je.id
LEFT JOIN note n ON n.journal_entry_id = je.id
LEFT JOIN scratch_pad sp ON sp.task_id = t.id
WHERE je.journal_id = 'journal-id';

-- 2. Get all tasks, notes, scratch pads, and journal entries belonging to a certain account
SELECT je.*, t.*, n.*, sp.*
FROM journal_entry je
JOIN journal j ON j.id = je.journal_id
JOIN account a ON a.account_id = j.account_id
LEFT JOIN task t ON t.journal_entry_id = je.id
LEFT JOIN note n ON n.journal_entry_id = je.id
LEFT JOIN scratch_pad sp ON sp.task_id = t.id
WHERE a.account_id = 'account-id';

-- 3. Get all tasks, notes, and reviews belonging to a journal entry
SELECT t.*, n.*, r.*
FROM journal_entry je
LEFT JOIN task t ON t.journal_entry_id = je.id
LEFT JOIN note n ON n.journal_entry_id = je.id
LEFT JOIN review r ON r.journal_entry_id = je.id
WHERE je.id = 'journal-entry-id';
