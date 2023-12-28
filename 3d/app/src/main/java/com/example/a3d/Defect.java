package com.example.a3d;

public class Defect {
    private int image;
    private String title;
    private String description;

    public Defect(int image, String title, String description) {
        this.image = image;
        this.title = title;
        this.description = description;
    }

    public int getImage() {
        return image;
    }

    public String getTitle() {
        return title;
    }

    public String getDescription() {
        return description;
    }
}
