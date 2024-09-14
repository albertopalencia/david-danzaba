namespace RealState.Application.PropertyImages.Dto;

public class PropertyImageDto
{
    public required byte[] File { get; set; }
    public bool Enabled { get; set; } = true;
}