import { z } from "zod";
//separate 5 spaces
//50 spaces for large differences

export const dateSchema = z.preprocess((val) => {
    if (val instanceof Date) return val;  // already a Date
    if (typeof val === "string" || typeof val === "number") return new Date(val); // convert string

    return val;
}, z.date());

















































//db
export const userSchema = z.object({
    //defaults
    id: z.string().min(1, "please add a user id"),

    //regular

    //null
    name: z.string().min(1).nullable(),
    email: z.string().email().nullable(),
    emailVerified: dateSchema.nullable(),
    image: z.string().min(1).nullable(),
})
export type userType = z.infer<typeof userSchema> & {
}

export const newUserSchema = userSchema.omit({ id: true })
export type newUserType = z.infer<typeof newUserSchema>

export const updateUserSchema = userSchema.omit({ id: true })
export type updateUserType = z.infer<typeof updateUserSchema>