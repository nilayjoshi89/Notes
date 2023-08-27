## 1. Different stages:
	1. Working area
		- Undo
			- Tracked
				- git restore <filename>
				- git checkout -- <filename>
				- git checkout HEAD <filename>
			- Untracked
				- rm (windows command)
		- Stage
			- git add <filename>
	
	2. Staged area
		- Unstage
			- Tracked
				- git restore --staged <filename>
				- git reset <filename>  (It is Mixed mode by default)
			- Untracked
				- git rm --cached <filename>
				- git reset <filename>  (It is Mixed mode by default)
		- Commit to repo
			- git commit -m "message here"
	
	3. Repo

## 2. Git Reset
	- Mixed
		- reset staging area
	- hard
		- reset staging/working area
	- soft
		- reset head only

## 3. Difference
	1. working v/s staged
		- git diff
	2. staged v/s repo
		- git diff --cached
	3. staged+unstaged v/s repo
		- git diff HEAD
	4. Branch diff
		- git diff branch1 branch2

## 4. History
	- git log --graph --decorate --oneline
	
	- git show <commit>
	- git show Head^ (Parent of Head)
	- git show Head^^ (Parent of Parent of Head)
	- git show Head~3
	
	- git blame