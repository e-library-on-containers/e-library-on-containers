
# Contributing to eLibraryOnContainers

Here are the guidelines we'd like you to follow while contributing to this project:
* [Submission Guidelines](#submission-guidelines)
	* [Issues](#issues)
	* [Branches](#branches)
	* [Pull Requests](#pull-requests)
* [Rules and Conventions](#rules-and-conventions)
	* [Commit messages](#commit-messages)
	* [Branch names](#branch-names)
	* [Pull Request names](#pull-request-names)
	* [Squash Commit messages](#squash-commit-messages)

## Submission Guidelines
### Issues
We use GitHub issues to keep track of all the work in this project. Before submitting new issue (either for a new feature or a bug fix), make sure that it doesn't already exist.
When submitting an issue remember to use a clear and easy to understand title and description. If issue is a feature request, apart from detailed description (if it's required), try to define it using standard user story template:
```
**As a** ...,
**I want to** ...
**So that** ...
```

*Remember to include appropriate labels*

### Branches
As a team we made a decision to use a [naming convention for branches](#branch-names) for more readability. Depending on how big a feature is, feature branch can be short- or long- lived (according to that, there may be multiple branches linked to the same issue, one being long-lived and other/others short-lived).

### Pull Requests
All pull requests must adhere to these requirements:
* **All checks must pass before merging**
* **Each pull request requires at least one maintainer's approval before merging**
* **Approvals are INVALIDATED after updating branch**
* **All pull request must be merged using squash and merge option**
* **Each pull request has to be linked to an issue**


## Rules and Conventions

### Commit messages
There are no conventions for commit messages inside a feature branch but *remember* that someone might continue working on your long-lived branch or take over an issue from you and work on your short-lived branch. Use clear and understandable commit messages.

### Branch names
All branches should be made as feature branches using convention `feature/{issue_id}/{branch-name-in-kebab-case}`.

### Pull Request names
All pull requests names should use template `#{issue_id} {title}`.

### Squash Commit messages
All squash commit messages should use template `#{issue_id} {title} (PR: {pr_id})`.