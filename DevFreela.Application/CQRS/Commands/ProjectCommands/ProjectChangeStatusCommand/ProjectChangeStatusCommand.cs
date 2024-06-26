﻿using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.ProjectChangeStatusCommand;

public class ProjectChangeStatusCommand : IRequest<bool>
{
    public ProjectChangeStatusCommand(Guid id, int status)
    {
        IdProject = id;
        Status = status;
    }

    public Guid IdProject { get; set; }
    public int Status { get; set; }
}

